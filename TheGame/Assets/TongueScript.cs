using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueScript : MonoBehaviour
{
    public float speed;
    public float tongueSpeed = 15f;

    public float tongueCoolDown = 0.5f;
    public float tongueTimer = 1f;
    public float originalTongueTimer = 1f;
    public Transform originalPosition;
    public GameObject scalingOject;
    public Vector3 hitPoint;

    public GameObject attachedObject;

    public PlayerScript playerScript;

    public bool tongueReturned = false;

    public bool attached = false;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        originalPosition = GameObject.FindGameObjectWithTag("Player").transform;
        originalTongueTimer = tongueTimer;
        scalingOject = GameObject.Find("TongueStretch");
        transform.position = new Vector3(originalPosition.position.x, originalPosition.position.y + 1f, originalPosition.position.z);
        scalingOject.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        float dist = Vector3.Distance(scalingOject.transform.position, transform.position);
        scalingOject.transform.localScale = new Vector3(transform.localScale.x, dist*3.5f, transform.localScale.z);

        tongueTimer -= Time.deltaTime;
        transform.Translate(originalPosition.forward * speed * Time.deltaTime);

        if(GetComponentInChildren<Renderer>().enabled)
        {
            transform.position = new Vector3(transform.position.x, originalPosition.position.y+1f, transform.position.z);
        }

        if (tongueTimer <= 0f || attachedObject != null)
        {
            if(!tongueReturned)
            {
                speed = -tongueSpeed;
            }
             
            else if(tongueReturned)
            {
                speed = 0f;
                originalPosition.GetComponentInChildren<Animator>().SetBool("Eat", false);
            }           
        }

        else if (Input.GetButton("Fire1") && tongueTimer > 0f)
        {
            tongueReturned = false;
            scalingOject.GetComponent<Renderer>().enabled = true;
            originalPosition.GetComponentInChildren<Animator>().SetBool("Eat",true);
            gameObject.GetComponentInChildren<Collider>().enabled = true;
            gameObject.GetComponentInChildren<Renderer>().enabled = true;
            playerScript.canMove = false;
            speed = tongueSpeed;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            playerScript.canMove = false;
            tongueReturned = false;
            transform.position = new Vector3(originalPosition.position.x, originalPosition.position.y+1f, originalPosition.position.z);
            tongueTimer = originalTongueTimer;
            gameObject.GetComponentInChildren<Renderer>().enabled = true;
            gameObject.GetComponentInChildren<Collider>().enabled = true;
        }

        else if(Input.GetButtonUp("Fire1") && !attached)
        {           
            speed = -tongueSpeed;
        }

        if(attached)
        {
            transform.position = hitPoint;
            originalPosition.transform.LookAt(transform.position);
            originalPosition.transform.Translate(transform.forward * tongueSpeed * Time.deltaTime);
            originalPosition.GetComponent<Rigidbody>().isKinematic = true;
            originalPosition.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponentInChildren<Collider>().enabled = false;
            gameObject.GetComponentInChildren<Renderer>().enabled = false;
            if (originalPosition.parent != null)
            {
                originalPosition.parent = null;
            }
        }

        else if (!attached)
        {
            originalPosition.GetComponent<Rigidbody>().isKinematic = false;
            originalPosition.GetComponent<Rigidbody>().useGravity = true;
        }

        if(attachedObject != null)
        {
            attachedObject.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerScript.canMove = true;
            tongueReturned = true;
            attached = false;
            speed = 0f;
            originalPosition.GetComponent<Rigidbody>().isKinematic = false;
            originalPosition.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponentInChildren<Collider>().enabled = false;
            gameObject.GetComponentInChildren<Renderer>().enabled = false;
            scalingOject.GetComponent<Renderer>().enabled = false;

            if (attachedObject != null)
            {
                Destroy(attachedObject);
            }
        }

        if (other.gameObject.tag == "AttachPoint")
        {
            playerScript.canMove = false;
            attached = true;
            hitPoint = other.transform.position;
        }

        if (other.gameObject.tag == "Enemy" && attachedObject == null)
        {
            attachedObject = other.gameObject;
            attachedObject.GetComponent<Collider>().isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "AttachPoint")
        {
            attached = false;
            transform.position = new Vector3(originalPosition.position.x, originalPosition.position.y + 1f, originalPosition.position.z);
            originalPosition.GetComponent<Rigidbody>().isKinematic = false;
            originalPosition.GetComponent<Rigidbody>().useGravity = true;
        }

    }
}
