using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour {

	public GameObject spawnPoint;

	public void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "Player(Clone)") {
			Debug.Log("New checkpoint set");

			spawnPoint.transform.position = transform.position;
			spawnPoint.transform.rotation = transform.rotation;

			Destroy(this.gameObject);
		}
	}
}
