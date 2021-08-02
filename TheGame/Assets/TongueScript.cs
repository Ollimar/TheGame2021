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
    public GameObject mouth;
    public Transform tonguePosition;
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
        tonguePosition = GameObject.Find("TonguePosition").transform;
        originalTongueTimer = tongueTimer;
        scalingOject = GameObject.Find("TongueStretch");
        transform.position = new Vector3(tonguePosition.position.x, tonguePosition.position.y, tonguePosition.position.z);
        scalingOject.GetComponent<Renderer>().enabled = false;
        mouth = GameObject.Find("MouthOpenAnimated");
        mouth.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        float dist = Vector3.Distance(scalingOject.transform.position, transform.position);
        scalingOject.transform.localScale = new Vector3(transform.localScale.x, dist*3.3f, transform.localScale.z);

        tongueTimer -= Time.deltaTime;

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
            mouth.SetActive(true);
            originalPosition.GetComponentInChildren<Animator>().SetBool("Eat",true);
            gameObject.GetComponentInChildren<Collider>().enabled = true;
            gameObject.GetComponentInChildren<Renderer>().enabled = true;
            playerScript.canMove = false;
            speed = tongueSpeed;
        }
        

        if (Input.GetButtonDown("Fire1"))
        {
            playerScript.canMove = false;
            scalingOject.GetComponent<Renderer>().enabled = true;
            transform.position = new Vector3(tonguePosition.position.x, tonguePosition.position.y, tonguePosition.position.z);
            tongueTimer = originalTongueTimer;
            gameObject.GetComponentInChildren<Renderer>().enabled = true;
            gameObject.GetComponentInChildren<Collider>().enabled = true;

        }

        
        else if(Input.GetButtonUp("Fire1") && !attached )
        {           
            speed = -tongueSpeed;
        }
        

        if(attached)
        {
            transform.position = hitPoint;
            originalPosition.transform.LookAt(transform.position);
            originalPosition.transform.Translate(transform.forward * tongueSpeed * Time.deltaTime);
            playerScript.LaunchEffectStart();
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
            playerScript.LaunchEffectStop();
        }

        if(attachedObject != null)
        {
            attachedObject.transform.position = transform.position;
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(originalPosition.forward * speed*Time.deltaTime);
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
            transform.position = new Vector3(tonguePosition.position.x, tonguePosition.position.y, tonguePosition.position.z);

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
            tongueTimer = 0f;
            if (attachedObject.GetComponent<Animator>())
            {
                attachedObject.GetComponent<Animator>().SetBool("EnemyEaten",true);
            }
        }

        
        if(transform.eulerAngles.x >0f || transform.eulerAngles.x < 0f || transform.eulerAngles.z > 0f || transform.eulerAngles.z < 0f)
        {
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y,0f);
        }

        if(other.gameObject.tag == "Untagged")
        {
            tongueTimer = 0f;
        }

        mouth.SetActive(false);

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "AttachPoint")
        {
            attached = false;
            transform.position = new Vector3(tonguePosition.position.x, tonguePosition.position.y, tonguePosition.position.z);
            originalPosition.GetComponent<Rigidbody>().isKinematic = false;
            originalPosition.GetComponent<Rigidbody>().useGravity = true;
        }

    }

    public IEnumerator Return()
    {
        yield return new WaitForSeconds(tongueTimer);
        speed = -tongueSpeed;
    }
}
