using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricFloorScript : MonoBehaviour {

	[Range(0.0f, 10.0f)]
	public float toggleOnSpeed;
	[Range(0.0f, 10.0f)]
	public float toggleOffSpeed;

	private float counter;
	private float currentCounter;
	private bool active = false;
	private GameObject electroFloor, killTrigger;

	// Use this for initialization
	void Start () {
		counter = toggleOnSpeed;
		currentCounter = 0.0f;	

		electroFloor = gameObject.transform.GetChild(0).gameObject;
		killTrigger = gameObject.transform.GetChild(1).gameObject;

		electroFloor.SetActive(false);
		killTrigger.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		currentCounter += Time.deltaTime;

		if (currentCounter >= counter) {
			currentCounter = 0.0f;
			toggleCurrent();
		}
	}

	private void toggleCurrent() {
		if (active) {
			active = false;

			counter = toggleOnSpeed;
			electroFloor.SetActive(false);
			//killTrigger.SetActive(false);

			gameObject.GetComponent<AudioSource>().Stop();
		} else {
			active = true;
			
			counter = toggleOffSpeed;
			electroFloor.SetActive(true);
			//killTrigger.SetActive(true);

			gameObject.GetComponent<AudioSource>().Play();
		}
	}
}
