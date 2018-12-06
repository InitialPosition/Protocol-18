using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 10.0f;
	public float jumpSpeed = 5.0f;
	float hInput, vInput;

	private float distToGround;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		rb = GetComponent<Rigidbody>();

		distToGround = gameObject.GetComponent<Collider>().bounds.extents.y;
	}
	
	// Update is called once per frame
	void Update () {
		hInput = Input.GetAxis("Horizontal") * speed;
		vInput = Input.GetAxis("Vertical") * speed;

		hInput *= Time.deltaTime;
		vInput *= Time.deltaTime;

		if (Input.GetButtonDown("Jump") && isGrounded()) {
			rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
		}

		transform.Translate(hInput, 0, vInput);

		if (Input.GetKeyDown("escape")) {
			Cursor.lockState = CursorLockMode.None;
		}
	}

	private bool isGrounded() {
   		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
 	}
}
