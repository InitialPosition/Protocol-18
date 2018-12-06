using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerScript : MonoBehaviour {
    [Tooltip ("Use this to determine, how big the random numbers can get")]
    [Range (5, 100)]
    public int range;
    [Tooltip("Use this to determine, how high the random number has to be at least. The lower the more often the light will flicker")]
    [Range(5, 99)]
    public int target;
    private Light lightTarget;
    private bool lightStatus;
	// Use this for initialization
	void Start () {
        lightTarget = gameObject.GetComponent<Light>();
        lightStatus = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (lightTarget != null)
        {
            // These are some ideas for random generated numbers
            //Random.Range(0, 2) == 1
            //Time.time * Random.Range(0.0f, 10.0f) % 3
            //Debug.Log(Time.time * Random.Range(0.0f, 10.0f) % 3);
            if ( Random.Range(0, range) > target)
            {
                lightStatus = !lightStatus;
            }

            lightTarget.enabled = lightStatus;
        }
	}
}
