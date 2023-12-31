using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bitgem.VFX.StylisedWater;

public class circlingShark : InfBadMath
{
    private GameObject _BoatRig;

    private Vector3 _Orbit;
    private float _Angle;
    private RaycastHit _Raycast;

    private bool reachedSurface = false;

    public string _Species = "Shark";

    public float distance = 20f;
    private float distanceSin;
    public float speed = .1f;
    public float _UpSpeed;

    private AudioSource _AudioSource;
    private float _choice;

    public Bait _Bait = null;

    //death
    private int _HitPoints = 2;
    public bool _Dead = false;

    
    void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
        _BoatRig = GameController.Instance.BoatRig;

    }

    Transform TopParent(Transform transform)
    {
        while (transform.parent == true)
            transform = transform.parent;
        return transform;
    }

    void CheckForBait()
    {
        _Bait = GameController.Instance.checkForBaits(_Species, transform);
        if (_Bait != null && _choice + 4 <= Time.time)
        {
            Physics.Raycast(transform.position, _Bait.transform.position - transform.position, out _Raycast);
            if (_Raycast.transform != null)
            {
                if (_Raycast.transform.parent)
                {
                    Transform transform = TopParent(_Raycast.transform);
                    if (transform.GetComponent<Bait>())
                    {
                        if (transform.GetComponent<Bait>() == _Bait)
                        {
                            Bait();
                            return;
                        }
                    }
                    if (_Bait.transform.GetComponent<Bait>())
                    {
                        if (_Bait.transform.GetComponent<Bait>() == _Bait)
                        {
                            Bait();
                            return;
                        }
                    }
                }
            }
            _choice = Time.time;
        }
        NoBait();
    }

    void Bait()
    {
        transform.LookAt(Vector3.MoveTowards(transform.position, _Bait.transform.position, Time.deltaTime * speed));
        transform.position = Vector3.MoveTowards(transform.position, _Bait.transform.position, Time.deltaTime * speed);

        if (_Bait.transform.tag == "Player" && _AudioSource.isPlaying == false)
        {
            _AudioSource.Play();
        }
        else if (_Bait.transform.tag != "Player" && _AudioSource.isPlaying == true)
        {
            _AudioSource.Stop();
        }

        if (Vector3.Distance(transform.position, _Bait.transform.position) <= 5)
        {
            if (_Bait.transform.tag == "Player")
            {
                _Bait.GetComponent<lifeJacket>().eatenByShark();
            } else {
                Destroy(_Bait.gameObject);
                _Angle = AngleOnCircle(transform.position.x, transform.position.z, Vector3.Distance(_BoatRig.transform.position, transform.position));
            }
        }
    }

    void NoBait()
    {
        _AudioSource.Stop();
        distanceSin = Mathf.Sin(Time.time) * 2 + distance;
        _Angle += Time.deltaTime * speed * 0.05f;
        _Orbit = new Vector3(Mathf.Sin(_Angle) * distanceSin, transform.position.y + _UpSpeed, Mathf.Cos(_Angle) * distanceSin);
        transform.LookAt(Vector3.MoveTowards(transform.position, _Orbit, Time.deltaTime * speed));
        transform.position = Vector3.MoveTowards(transform.position, _Orbit, Time.deltaTime * speed);
    }

    void Update()
    {
        if (_Dead)
        {
            transform.position += Vector3.down * Time.deltaTime;
            transform.Rotate(Vector3.forward, 90 * Time.deltaTime);
            Destroy(gameObject, 2);
            GetComponent<WateverVolumeFloater>().enabled = false;
            return;
        }

        _Bait = GameController.Instance.checkForBaits(_Species, transform);
        CheckForBait();

        if ( !reachedSurface )
        {
            if (transform.position.y > -0.5f)
            {
                GetComponent<WateverVolumeFloater>().enabled = true;
                reachedSurface = true;
                _UpSpeed = 0;
                GameController.Instance.gcNarrator.Tell("GreatWhite");
            }
        }
    }

    public void Damage()
    {
        _HitPoints--;
        if (_HitPoints < 1)
            _Dead = true;
    }
}
