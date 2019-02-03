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
    public GameObject currentSpawn;
	float hInput, vInput;

    private Rigidbody rb;
    private Ray ray;
    private RaycastHit hit;
    private GameObject item;        // What item the player is currently holding
    private GameObject temp;
    private float distToGround;
    private bool handsFull;         // States if the hands of the player are full
    private GameObject hud;

    // Use this for initialization
    void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		rb = GetComponent<Rigidbody>();
		distToGround = gameObject.GetComponent<Collider>().bounds.extents.y;
        handsFull = false;

        hud = GameObject.Find("HUD");
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
            hud.GetComponent<PauseMenuScript>().openPauseMenu();
		}

        if (Input.GetMouseButtonDown(0) && handsFull)
        {
            ThrowTarget();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (handsFull)
            {
                PutTargetDown();
            }
            else
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, pickUpRange))
                {
                    if (hit.transform.tag == "InteractableObject" || hit.transform.tag == "Tetrahedra")
                    {
                        //Debug.Log(hit.collider.gameObject.name);
                        PickUpTarget(hit.collider.gameObject);
                    } else if(hit.transform.tag == "EnergyStand")
                    {
                        PickUpTarget(hit.transform.GetComponent<EnergyStandScript>().ReleaseObjectToPlayer());
                    }
                }  
            }        
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hazard")
        {
            Debug.Log("Hazard hit, respawning...");

            gameObject.GetComponent<AudioSource>().Play();
            if (handsFull) {
                PutTargetDown();
            }

            this.transform.position = currentSpawn.transform.position;
            this.transform.rotation = currentSpawn.transform.rotation;
        }
    }

    public GameObject GetItem()
    {
        return item;
    }

    public void CallToPutTargetDown()
    {
        if (handsFull)
        {
            PutTargetDown();
        }
        Debug.Log("Called to Unitem");
    }

    private bool IsGrounded() {
   		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
 	}

    private void PickUpTarget(GameObject target)
    {
        handsFull = true;
        item = target;
        // Edit the picked up object to work as expected while in the players hands
        if (item.gameObject.tag == "Tetrahedra")
        {
            item.transform.localScale = new Vector3(60, 60, 60);
        }
        ManageItem();
    }

    private void PutTargetDown()
    {
        handsFull = false;
        ManageItem();
        if (item.gameObject.tag == "Tetrahedra")
        {
            item.transform.localScale = new Vector3(100, 100, 100); // Shouldnt be hard coded, but is for now
        } 
        item = null;
    }

    private void ThrowTarget()
    {
        temp = item;
        PutTargetDown();
        temp.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce);
        temp = null;
    }

    private void ManageItem()
    {
        item.GetComponent<Collider>().isTrigger = handsFull;        // These lines 
        item.GetComponent<Rigidbody>().isKinematic = handsFull;     // alter the rigidbody, so
        item.GetComponent<Rigidbody>().useGravity = !handsFull;     // the obejct wont move in the hands of the player
        if (handsFull)
        {
            item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            item.transform.parent = transform.GetChild(0).GetChild(0).transform; // Sets the Holdpoint as the parent of object
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
        } else
        {
            item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            item.transform.parent = null; // if the object had a parent at some point, then the parent needs to be saved and used here
        } 
    }
}
