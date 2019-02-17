using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript : MonoBehaviour {

    //[HideInInspector]
    //public enum directionsOfForce {alongXAxis, alongYAxis, alongZAxis, contraryToXAxis, contraryToZAxis, contraryToYAxis}

    private List<GameObject> objectList = new List<GameObject>();

    #region Trigger Functions
    public void OnTriggerEnter(Collider other)
    {
        if (!objectList.Contains(other.gameObject) && other.GetComponent<Rigidbody>() != null)
        {
            objectList.Add(other.gameObject);
            Debug.Log("Added " + other.gameObject.name + " to the list");
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (!objectList.Contains(other.gameObject) && other.GetComponent<Rigidbody>() != null)
        {
            objectList.Add(other.gameObject);
            Debug.Log("Added " + other.gameObject.name + " to the list");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (objectList.Contains(other.gameObject))
        {
            RemoveConstantForceFromObject(other.gameObject);
            objectList.Remove(other.gameObject);
        }
    }
    #endregion

    public void ThrowAlongAxis(int i)
    {
        switch (i)
        {
            case 0:
                ForceObjectsInList(-70, 0, true);
                break;
            case 1:
                ForceObjectsInList(70, 0, true);
                break;
        }
    }

    public void RemoveConstantForceFromObjectsFromList()
    {
        foreach (GameObject obj in objectList)
        {
            RemoveConstantForceFromObject(obj);
        }
    }

    private void RemoveConstantForceFromObject(GameObject obj)
    {
        if (obj.GetComponent<ConstantForce>() != null)
        {
            Destroy(obj.GetComponent<ConstantForce>());
        }
    }

    private void ForceObjectsInList(float val, int axis, bool removeGravity)
    {
        float grav; // This value is used to counter the gravity if needed.
        float valForXAxis = 0;
        float valForYAxis = 0;
        float valForZAxis = 0;

        if (removeGravity)
        {
            grav = 9.81f;
        }
        else
        {
            grav = 0f;
        }

        switch (axis)
        {
            case 0:                             // 0 is for along the X axis
                valForXAxis = val;
                break;
            case 1:                             // 1 is for along Y axis
                valForYAxis = val + grav;
                break;  
            case 2:                             // 2 is for along Z axis
                valForZAxis = val;
                break;
        }

        foreach (GameObject obj in objectList)
        {
            if (obj.GetComponent<ConstantForce>() == null)
            {
                obj.AddComponent<ConstantForce>().force = new Vector3(valForXAxis, valForYAxis, valForZAxis);
            }
            else
            {
                obj.GetComponent<ConstantForce>().force = new Vector3(valForXAxis, valForYAxis, valForZAxis);
            }
        }
    }

}
