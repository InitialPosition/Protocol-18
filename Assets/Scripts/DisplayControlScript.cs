using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DisplayControlScript : MonoBehaviour {

    public UnityEvent onPress;
    public UnityEvent onPressAgain;

    private bool pressed = false; // Is true, if the display has been pressed before
    // Use this for initialization
    void Start () {
        if (onPress == null)
        {
            onPress = new UnityEvent();
        }
        if (onPressAgain == null)
        {
            onPressAgain = new UnityEvent();
        }
    }

    public void CallDisplayFunction()
    {
        if(pressed)
        {
            onPressAgain.Invoke();
        }else
        {
            onPress.Invoke();
        }
        pressed = !pressed;
    }
}
