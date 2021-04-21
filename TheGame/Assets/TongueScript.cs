using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueScript : MonoBehaviour
{
    public float speed;
    public float tongueSpeed = 15f;

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
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(originalPosition.transform.position, transform.position*2f);
        print(dist);
        tongueTimer -= Time.deltaTime;
        transform.Translate(originalPosition.forward * speed * Time.deltaTime);
        //scalingOject.transform.localScale = new Vector3(1f, dist, 1f);

        if (tongueTimer <= 0f && !attached && !tongueReturned)
        {
            speed = -tongueSpeed;
            //playerScript.canMove = true;           
        }
 
        if(Input.GetButtonDown("Fire1"))
        {
            playerScript.canMove = false;
            tongueReturned = false;
            transform.position = originalPosition.position;
            tongueTimer = originalTongueTimer;
            gameObject.GetComponentInChildren<Renderer>().enabled = true;
        }

        if (Input.GetButton("Fire1") && tongueTimer >0f)
        {
            playerScript.canMove = false;
            speed = tongueSpeed;
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
            //gameObject.GetComponentInChildren<Renderer>().enabled = false;

            if(attachedObject != null)
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

        if (other.gameObject.tag == "Enemy")
        {
            attachedObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "AttachPoint")
        {
            attached = false;
        }
    }
}
