using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 10.0f;
	public float jumpSpeed = 5.0f;
    [Range (100f, 10000.0f)]
    public float throwForce = 1.0f;
    [Range (0.1f, 10.0f)]
    public float pickUpRange;
	float hInput, vInput;

    private Rigidbody rb;
    private Ray ray;
    private RaycastHit hit;
    private GameObject load;
    private GameObject temp;
    private float distToGround;
    private bool loaded;

    // Use this for initialization
    void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		rb = GetComponent<Rigidbody>();
		distToGround = gameObject.GetComponent<Collider>().bounds.extents.y;
        loaded = false;
	}
	
	// Update is called once per frame
	void Update () {
		hInput = Input.GetAxis("Horizontal") * speed;
		vInput = Input.GetAxis("Vertical") * speed;

		hInput *= Time.deltaTime;
		vInput *= Time.deltaTime;
        if (Input.GetButtonDown("Jump") && IsGrounded()) {
			rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
		}

		transform.Translate(hInput, 0, vInput);

		if (Input.GetKeyDown("escape")) {
			Cursor.lockState = CursorLockMode.None;
		}

        if (Input.GetMouseButtonDown(0) && loaded)
        {
            ThrowTarget();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (loaded)
            {
                UnloadTarget();
            }
            else
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, pickUpRange))
                {
                    if (hit.transform.tag == "InteractableObject" || hit.transform.tag == "Tetrahedra")
                    {
                        //Debug.Log(hit.collider.gameObject.name);
                        LoadTarget(hit.collider.gameObject);
                    }else if(hit.transform.tag == "EnergyStand")
                    {
                        LoadTarget(hit.transform.GetComponent<EnergyStandScript>().ReleaseObjectToPlayer());
                    }
                }  
            }        
        }
    }

    public GameObject GetLoad()
    {
        return load;
    }

    public void CallToUnload()
    {
        if (loaded)
        {
            UnloadTarget();
        }
        Debug.Log("Called to Unload");
    }

    private bool IsGrounded() {
   		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
 	}

    private void LoadTarget(GameObject target)
    {
        loaded = true;
        load = target;
        // Edit the picked up object to work as expected while in the players hands
        if (load.gameObject.tag == "Tetrahedra")
        {
            load.transform.localScale = new Vector3(60, 60, 60);
        }
        load.transform.parent = transform.GetChild(0).GetChild(0).transform; // Sets the Holdpoint as the parent of object
        load.GetComponent<Collider>().isTrigger = true;
        load.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        load.transform.localPosition = Vector3.zero;
    }

    private void UnloadTarget()
    {
        load.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        load.GetComponent<Collider>().isTrigger = false;
        load.transform.parent = null; // if the object had a parent at some point, then the parent needs to be saved and used here
        if (load.gameObject.tag == "Tetrahedra")
        {
            load.transform.localScale = new Vector3(100, 100, 100); // Shouldnt be hard coded, but is for now
        }
        loaded = false;
        load = null;
    }

    private void ThrowTarget()
    {
        temp = load;
        UnloadTarget();
        temp.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce);
        temp = null;

    }
}
