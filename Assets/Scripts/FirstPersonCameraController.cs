using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCameraController : MonoBehaviour {
	private float minX = -90f;
	private float maxX = 90f;
	private float minY = -360f;
	private float maxY = 360f;

	private float sensitivity = 7f;

	private Camera cam;
	float rotationX = 0f;
	float rotationY = 0f;

	void Start() {
		cam = GetComponent<Camera>();
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update() {
		rotationX += Input.GetAxis("Mouse Y") * sensitivity;
		rotationY += Input.GetAxis("Mouse X") * sensitivity;

		rotationX = Mathf.Clamp(rotationX, minX, maxX);

		transform.localEulerAngles = new Vector3(0, rotationY, 0);
		transform.parent.transform.localEulerAngles = new Vector3(0, rotationY, 0);
		cam.transform.localEulerAngles = new Vector3(-rotationX, 0, 0);
	}
}