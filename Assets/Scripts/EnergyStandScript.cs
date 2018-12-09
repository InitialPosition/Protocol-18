using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyStandScript : MonoBehaviour {

    [Tooltip ("Every object in this list will be deactivated on task completion")]
    public List<GameObject> deactivationList = new List<GameObject>();
    [Tooltip("Every light in this list will stop flicker and will get brighter on task completion")]
    public List<Light> lightList = new List<Light>();
    [Tooltip("Every audio source will be played on task completion")]
    public List<AudioSource> audioList = new List<AudioSource>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tetrahedra")
        {
            // Deactivate the objects
            foreach (GameObject obj in deactivationList)
            {
                obj.SetActive(false);
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
            other.gameObject.transform.position = transform.position;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
