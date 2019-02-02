using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {

	public GameObject LaserBeam;

	private GameObject laserChild, laserBeam;
	private float maxLaserLength = 100f;
	private float lengthFix = 5.5f;
	private RaycastHit foundWall;
	void Start () {
		// create laser beam as child
		laserBeam = (GameObject) Instantiate(LaserBeam, transform.position, transform.rotation);
		laserChild = laserBeam.transform.GetChild(0).gameObject;
		laserChild.gameObject.tag = "Hazard";

		// get distance to next wall and scale laser accordingly
		Ray distanceRay = new Ray(transform.position, new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z));
		if (Physics.Raycast(distanceRay, out foundWall)) {
			laserBeam.transform.localScale = new Vector3(1, 1, -foundWall.distance * lengthFix);
		}
	}
}
