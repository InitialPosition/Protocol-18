using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour {

	private Vector3 currentMovement, currentPosition;
	private float speedModifier;
    [Range(0.0f, 15.0f)]
	public float groundSpeedModifier = 7.0f;
    [Range(0.0f, 15.0f)]
    public float airSpeedModifier = 8.0f;
    [Range(5.0f, 20.0f)]
    public float jumpForce = 7.0f;
	private float distToGround;
	private GameObject myCam;

	// Use this for initialization
	void Start () {
		distToGround = gameObject.GetComponent<Collider>().bounds.extents.y;
        Debug.Log(distToGround);
		myCam = transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (isGrounded()) {
			speedModifier = groundSpeedModifier;

			if (isJumping()) {
                gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
			}
		} else {
			speedModifier = airSpeedModifier;
		}

		currentMovement = getInput();
		currentMovement = currentMovement / speedModifier;
        //Debug.Log(currentMovement);
		transform.Translate(currentMovement);
	}

	private Vector3 getInput() {
		// initialize a new vector
		Vector3 movementVector = new Vector3();

		// add the correct movement vectors to the main vector
		if (Input.GetKey(GlobalVars.move_forward)) {
			movementVector += new Vector3(0, 0, 1); // 0 0 1
		}
		if (Input.GetKey(GlobalVars.move_backward)) {
			movementVector += new Vector3(0, 0, -1); // 0 0 -1
		}
		if (Input.GetKey(GlobalVars.move_left)) {
			movementVector += new Vector3(-1, 0, 0); // -1 0 0
		}
		if (Input.GetKey(GlobalVars.move_right)) {
			movementVector += new Vector3(1, 0, 0); // 1 0 0 
		}
		
		// return the vector
		return movementVector;
	}

	private bool isJumping() {
		return Input.GetKeyDown(GlobalVars.move_jump);
	}

	private bool isGrounded() {
        bool ground = Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
        //Debug.Log("isGrounded: " + ground);
        return ground;
 	}
}
