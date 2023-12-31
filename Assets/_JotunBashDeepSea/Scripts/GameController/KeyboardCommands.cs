using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardCommands : MonoBehaviour
{

    void OnWeatherIncrease(InputValue value)
    {
        if (value.isPressed)
        {
            GameController.Instance.gcWeather.sliderWind += 0.1f;
        }
    }

    void OnWeatherDecrease(InputValue value)
    {
        if (value.isPressed)
        {
            GameController.Instance.gcWeather.sliderWind -= 0.1f;
        }
    }

    void OnResetPlayerPos(InputValue value)
    {
        if (value.isPressed)
        {
            GameController.Instance.player.GetComponent<lifeJacket>().resetPlayer();
        }
    }

    void OnAddTime(InputValue value)
    {
        if (value.isPressed)
        {
            GameController.Instance.secondsLeft += 60;
        }
    }

    void OnZeroTime(InputValue value)
    {
        if (value.isPressed)
        {
            GameController.Instance.secondsLeft = 0;
        }
    }

    void OnFruitNinja(InputValue value)
    {
        GameController.Instance.gcInventory.Logs = 100;
        GameController.Instance.gcInventory.Planks = 100;
        GameController.Instance.gcInventory.Barrels = 100;
    }
}
