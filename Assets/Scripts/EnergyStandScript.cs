using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyStandScript : MonoBehaviour
{

    [Tooltip("Every object in this list will be deactivated on task completion")]
    public List<GameObject> deactivationList = new List<GameObject>();
    [Tooltip("Every light in this list will stop flicker and will get brighter on task completion")]
    public List<Light> lightList = new List<Light>();
    [Tooltip("Every audio source will be played on task completion")]
    public List<AudioSource> audioList = new List<AudioSource>();

    private GameObject objectInUse;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tetrahedra")
        {
            UseObject(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == objectInUse)
        {
            ReleaseObject();
        }
    }

    public GameObject ReleaseObjectToPlayer()
    {
        var temp = objectInUse;
        ReleaseObject();
        return temp;
    } 

    private void UseObject(Collider obj)
    {
        if (obj.gameObject == GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetItem())
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().CallToPutTargetDown();
        }

        // Deactivate the objects
        foreach (GameObject objToDeactivate in deactivationList)
        {
            objToDeactivate.SetActive(false);
        }

        // Set the lights, so they dont flicker and are brighter
        foreach (Light lig in lightList)
        {
            lig.gameObject.GetComponent<FlickerScript>().enabled = false;
            lig.intensity += 0.5f;
        }

        // Set every audio Source to play
        foreach (AudioSource sound in audioList)
        {
            sound.Play();
        }

        // fix the tetrahedra to the energy stand
        obj.gameObject.transform.position = transform.position;
        objectInUse = obj.gameObject;
        obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    private void ReleaseObject()
    {
        // Deactivate the objects
        foreach (GameObject objToDeactivate in deactivationList)
        {
            objToDeactivate.SetActive(true);
        }

        // Set the lights, so they dont flicker and are brighter
        foreach (Light lig in lightList)
        {
            lig.gameObject.GetComponent<FlickerScript>().enabled = true;
            lig.intensity -= 0.5f;
        }

        // Set every audio Source to play
        foreach (AudioSource sound in audioList)
        {
            sound.Stop();
        }

        // fix the tetrahedra to the energy stand
        objectInUse.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        objectInUse = null;
    }
}
