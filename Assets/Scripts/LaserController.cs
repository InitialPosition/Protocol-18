using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{

    public GameObject LaserBeam;

    private GameObject laserChild, laserBeam;
    private float laserLength = 26f;
    void Start()
    {
        // create laser beam as child
        laserBeam = (GameObject)Instantiate(LaserBeam, transform.position, transform.rotation);
        laserChild = laserBeam.transform.GetChild(0).gameObject;
        laserChild.gameObject.tag = "Hazard";

        laserBeam.transform.localScale = new Vector3(1, 1, laserLength);
    }
}