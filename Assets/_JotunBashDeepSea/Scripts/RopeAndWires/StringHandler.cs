using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringHandler : MonoBehaviour
{

    public Rigidbody stringAttach;

    private LineRenderer myLine;

    public void simpleLineToAttachment()
    {
        myLine.SetPosition(0, transform.position);
        myLine.SetPosition(1, stringAttach.transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        myLine = GetComponent<LineRenderer>();
        myLine.positionCount = 2;
        myLine.SetWidth(.005f, .005f);
    }

    // Update is called once per frame
    void Update()
    {
        simpleLineToAttachment();
    }
}
