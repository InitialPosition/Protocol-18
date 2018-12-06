using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationTriggerScript : MonoBehaviour {
    [Tooltip ("Every object in this list will be deactivated on game start and reactivated if something enters the trigger")]
    public List<GameObject> activationList = new List<GameObject>();

    private void Start()
    {
        foreach (GameObject obj in activationList)
        {
            obj.SetActive(false);
        }
    }

    private void OnTriggerEnter(object other)
    {
        foreach (GameObject obj in activationList)
        {
            obj.SetActive(true);
        }
    }
}
