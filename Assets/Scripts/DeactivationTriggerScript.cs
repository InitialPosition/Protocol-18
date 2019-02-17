using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivationTriggerScript : MonoBehaviour {
    [Tooltip("Every object in this list will be deactivated if something enters the trigger")]
    public List<GameObject> deactivationList = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        DeactivateObjectsFromList();
    }

    public void DeactivateObjectsFromList()
    {
        foreach (GameObject obj in deactivationList)
        {
            obj.SetActive(false);
        }
    }
}
