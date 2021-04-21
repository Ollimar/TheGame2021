using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueScript : MonoBehaviour
{
    public bool tongueOut = false;

    public float speed;
    public float tongueSpeed = 15f;

    public float tongueTimer = 0.5f;
    public float originalTongueTimer = 1f;
    public Transform tongue;
    public Transform tongueTip;
    public Transform originalPosition;
    public Vector3 hitPoint;

    public PlayerScript playerScript;

    public bool tongueReturned = false;

    public bool attached = false;

    public GameObject attachedEnemy;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        originalPosition = GameObject.FindGameObjectWithTag("Player").transform;
        originalTongueTimer = tongueTimer;
        transform.parent = originalPosition.transform;
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y+1, transform.parent.position.z);
        tongueTip = GameObject.Find("TongueTip").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetButtonDown("Fire1") && !tongueOut)
        {
            tongueOut = true;
            tongue.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            playerScript.canMove = false;
            tongue.GetComponent<Renderer>().enabled = true;
            tongueTip.GetComponent<Renderer>().enabled = true;
            tongueTip.GetComponent<Collider>().enabled = true;
            originalPosition.GetComponentInChildren<Animator>().SetBool("Eat", true);
        }

        if(tongueOut)
        {
            tongueTimer -= Time.deltaTime;
            tongue.localScale += new Vector3(0f, tongueSpeed, 0f)*Time.deltaTime;
            tongueTip.Translate(originalPosition.forward * tongueSpeed/4.5f * Time.deltaTime);
        }

        if(tongueTimer <= 0f && !attached || attachedEnemy != null && !attached)
        {
            tongueOut = false;
        }

        if(!tongueOut && tongue.localScale.y > 1f && !attached)
        {
            tongue.localScale -= new Vector3(0f, tongueSpeed, 0f) * Time.deltaTime;
            tongueTip.Translate(originalPosition.forward * -tongueSpeed/4.5f * Time.deltaTime);
        }

        if(tongue.localScale.y <= 1f)
        {
            tongueTimer = 0.25f;
            tongue.localScale = new Vector3(1f, 1f, 1f);
            originalPosition.GetComponentInChildren<Animator>().SetBool("Eat", false);
            tongueTip.position = new Vector3(originalPosition.position.x, originalPosition.position.y+1f, originalPosition.position.z);
            playerScript.canMove = true;
            tongue.GetComponent<Renderer>().enabled = false;
            if (attachedEnemy != null)
            {
                Destroy(attachedEnemy);
            }
            gameObject.GetComponentInChildren<Renderer>().enabled = false;
            tongueTip.GetComponentInChildren<Renderer>().enabled = false;
            tongueTip.GetComponent<Collider>().enabled = false;

        }

        if (attachedEnemy != null)
        {
            attachedEnemy.transform.position = transform.position;
        }

        if (attached)
        {
            //transform.position = hitPoint;
            //originalPosition.transform.LookAt(new Vector3(hitPoint.x,hitPoint.y,hitPoint.z));
            originalPosition.transform.Translate(originalPosition.forward * 10f * Time.deltaTime);
            originalPosition.GetComponent<Rigidbody>().isKinematic = true;
            originalPosition.GetComponent<Rigidbody>().useGravity = false;
        }

        else if (!attached)
        {
            originalPosition.GetComponent<Rigidbody>().isKinematic = false;
            originalPosition.GetComponent<Rigidbody>().useGravity = true;
        }

        /*
        tongueTimer -= Time.deltaTime;
        transform.Translate(originalPosition.forward * speed * Time.deltaTime);

        if (tongueTimer <= 0f && !attached && !tongueReturned)
        {
            speed = -tongueSpeed;
            playerScript.canMove = true;
            
        }
 
        if(Input.GetButtonDown("Fire1"))
        {
            playerScript.canMove = false;
            tongueReturned = false;
            transform.position = originalPosition.position;
            tongueTimer = originalTongueTimer;
            //gameObject.GetComponentInChildren<Renderer>().enabled = true;
        }

        if (Input.GetButton("Fire1") && tongueTimer >0f)
        {
            playerScript.canMove = false;
            tongue.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z + 1f * Time.deltaTime);
            speed = tongueSpeed;
        }

        else if(Input.GetButtonUp("Fire1") && !attached)
        {
            
            speed = -tongueSpeed;

        }




        */
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
        if(other.gameObject.tag == "Player")
        {
            playerScript.canMove = true;
            tongueReturned = true;
            attached = false;
            speed = 0f;
            originalPosition.GetComponent<Rigidbody>().isKinematic = false;
            originalPosition.GetComponent<Rigidbody>().useGravity = true;
            if(attachedEnemy != null)
            {
                Destroy(attachedEnemy);
            }
        }

        if (other.gameObject.tag == "Enemy")
        {
            attachedEnemy = other.transform.gameObject;
        }

        if (other.gameObject.tag == "AttachPoint")
        {
            playerScript.canMove = false;
            attached = true;
            hitPoint = other.transform.position;
        }
        */
    }


}
