using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 10.0f;
	public float jumpSpeed = 5.0f;
	float hInput, vInput;

    private Rigidbody rb;
    private Ray ray;
    private RaycastHit hit;
    private GameObject load;
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
        #region loading
        if (loaded && load != null)
        {
            load.transform.position = transform.GetChild(0).transform.position;
        }
        #endregion  // This fixes the target to the player every frame
        if (Input.GetButtonDown("Jump") && isGrounded()) {
			rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
		}

		transform.Translate(hInput, 0, vInput);

		if (Input.GetKeyDown("escape")) {
			Cursor.lockState = CursorLockMode.None;
		}

        if (Input.GetKeyDown(KeyCode.E))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.collider.gameObject.name);
                if(loaded)
                {
                    unloadTarget();
                }else
                {
                    loadTarget(hit.collider.gameObject);
                }
            }      
        }
    }

	private bool isGrounded() {
   		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
 	}

    private void loadTarget(GameObject target)
    {
        loaded = true;
        load = target;
        load.GetComponent<Collider>().isTrigger = true;
        load.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    private void unloadTarget()
    {
        load.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
       
        loaded = false;
        load = null;
    }
}
