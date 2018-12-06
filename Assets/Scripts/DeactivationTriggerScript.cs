using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivationTriggerScript : MonoBehaviour {
    [Tooltip("Every object in this list will be deactivated if something enters the trigger")]
    public List<GameObject> activationList = new List<GameObject>();

    private void OnTriggerEnter(object other)
    {
        foreach (GameObject obj in activationList)
        {
            obj.SetActive(false);
        }
    }
}
