using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnScript : MonoBehaviour {

	public GameObject player;

	private GameObject spawnedPlayer;

	// Use this for initialization
	void Start () {
		spawnedPlayer = (GameObject) Instantiate(player, transform.position, Quaternion.identity);

		Debug.Log("PLAYER ROTATION: " + spawnedPlayer.transform.localRotation);
		Debug.Log("TARGET ROTATION: " + transform.rotation);

		spawnedPlayer.transform.rotation = transform.rotation;

		spawnedPlayer.GetComponent<PlayerController>().currentSpawn = this.gameObject;

		Debug.Log("Added player " + spawnedPlayer.gameObject.name + ". Set player spawn instance to " + this.gameObject.name);
	}
}
