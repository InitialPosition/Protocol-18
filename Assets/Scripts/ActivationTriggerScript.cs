using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationTriggerScript : MonoBehaviour {
    [Tooltip ("Every object in this list will be deactivated on game start and reactivated if something enters the trigger")]
    public List<GameObject> activationList = new List<GameObject>();
    [Tooltip("Check this box, if you dont want the obejcts in this list to disapear on Awake")]
    public bool ignoreTargets;

    private void Start()
    {
        if (!ignoreTargets)
        {
            foreach (GameObject obj in activationList)
            {
                obj.SetActive(false);
            }
        }     
    }

    private void OnTriggerEnter(object other)
    {
        ActivateObjectsFromScript();
    }

    public void ActivateObjectsFromScript()
    {
        foreach (GameObject obj in activationList)
        {
            obj.SetActive(true);
        }
    }
}
