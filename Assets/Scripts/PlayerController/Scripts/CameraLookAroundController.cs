using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAroundController : MonoBehaviour {
	Vector2 mouseLook;
	Vector2 vSmoothing;

	[Range(1.0f, 10.0f)]
	public float sensitivity = 5.0f;

	[Range(1.0f, 5.0f)]
	public float smoothing = 2.0f;

	GameObject player;

	Vector2 md;
	public bool mouseUpdating;

	// Use this for initialization
	void Start () {
		player = this.transform.parent.gameObject;
		mouseUpdating = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (mouseUpdating) {
			md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

			md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
			vSmoothing.x = Mathf.Lerp(vSmoothing.x, md.x, 1.0f / smoothing);
			vSmoothing.y = Mathf.Lerp(vSmoothing.y, md.y, 1.0f / smoothing);
			mouseLook += vSmoothing;

			mouseLook.y = Mathf.Clamp(mouseLook.y, -90, 90);

			transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
			player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
		}
	}
}
