using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript : MonoBehaviour {
    [HideInInspector]
    public enum directionsOfForce { alongXAxis, alongYAxis, alongZAxis, contraryToXAxis, contraryToZAxis, contraryToYAxis, justRemoveGravity}
    public directionsOfForce axis;
    [Tooltip("Check this box to force all objects on awake")]
    public bool forceOnAwake;
    [Tooltip("Check this box, if you want all objetcs to lose their forces when they exit the collider ")]
    public bool removeForceOnExit;
    [Range (0.0f, 100.0f)]
    public float force;
    [Tooltip ("Check this box, if this script is supposed to conter the gravity aswell.")]
    public bool removeGravity;
    [Tooltip("Check this box to force new objects on enter.")]
    public bool forceNewObjects;

    private List<GameObject> objectList = new List<GameObject>();
    private float grav; // This value is used to counter the gravity if needed.
    private float valForXAxis = 0;
    private float valForYAxis = 0;
    private float valForZAxis = 0;
    private bool forced; // Is true, if the objects are pushed/thrown

    private void Start()
    {
        if(forceOnAwake)
        {
            ThrowAlongAxis();
        }
    }

    #region Trigger Functions
    public void OnTriggerEnter(Collider other)
    {
        if (!objectList.Contains(other.gameObject) && other.GetComponent<Rigidbody>() != null)
        {
            objectList.Add(other.gameObject);
            if (forced && forceNewObjects)
            {
                ForceLastObjectInList();
            }  
            Debug.Log("Added " + other.gameObject.name + " to the list of " + name);         
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (!objectList.Contains(other.gameObject) && other.GetComponent<Rigidbody>() != null)
        {
            objectList.Add(other.gameObject);
            Debug.Log("Added " + other.gameObject.name + " to the list of " + name);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (objectList.Contains(other.gameObject))
        {
            if(removeForceOnExit)
            {
                RemoveConstantForceFromObject(other.gameObject);
            }    
            objectList.Remove(other.gameObject);
            Debug.Log("Removed " + other.gameObject.name + " to the list of " + name);
        }
    }
    #endregion

    public void ThrowAlongAxis()
    {
        ForceObjectsInList();
    }

    public void RemoveConstantForceFromObjectsFromList()
    {
        forced = false;
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

    private void ForceObjectsInList()
    {
        forced = true;
        CalculateStrengthOfForce();
        foreach (GameObject obj in objectList)  // Uses the set values to change forces on the objects from the list
        {
            if (obj.GetComponent<ConstantForce>() == null) // Adds a ConstantForce script if the objects dosnt have one
            {
                obj.AddComponent<ConstantForce>().force = new Vector3(valForXAxis, valForYAxis, valForZAxis);
            }
            else
            {
                obj.GetComponent<ConstantForce>().force = new Vector3(valForXAxis, valForYAxis, valForZAxis);
            }
        }
    }

    private void ForceLastObjectInList()
    {
        CalculateStrengthOfForce();
        if (objectList[objectList.Count - 1].GetComponent<ConstantForce>() == null) // Adds a ConstantForce script if the objects dosnt have one
        {
            objectList[objectList.Count - 1].AddComponent<ConstantForce>().force = new Vector3(valForXAxis, valForYAxis, valForZAxis);
        }
        else
        {
            objectList[objectList.Count - 1].GetComponent<ConstantForce>().force = new Vector3(valForXAxis, valForYAxis, valForZAxis);
        }   
    }

    private void CalculateStrengthOfForce()
    {
        if (removeGravity)
        {
            grav = 9.81f;
        }
        else
        {
            grav = 0f;
        }

        switch (axis)   // Sets the values acording the selection
        {
            case directionsOfForce.alongXAxis:
                valForXAxis = force;
                break;
            case directionsOfForce.alongYAxis:
                valForYAxis = force + grav;
                break;
            case directionsOfForce.alongZAxis:
                valForZAxis = force;
                break;
            case directionsOfForce.contraryToXAxis:
                valForXAxis = -force;
                break;
            case directionsOfForce.contraryToYAxis:
                valForYAxis = -force + grav;
                break;
            case directionsOfForce.contraryToZAxis:
                valForZAxis = -force;
                break;
            case directionsOfForce.justRemoveGravity:
                valForYAxis = 9.81f;     
                break;
        }
    }

}
