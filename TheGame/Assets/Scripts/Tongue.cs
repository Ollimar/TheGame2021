using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    public bool canLick = true;
    public bool tongueActive = false;

    private PlayerScript player;
    public GameObject playerObject;

    public GameObject tongueStart;
    public GameObject tongueEnd;

    public GameObject tongueStretch;

    public GameObject tonguePosition;
    public GameObject attachedObject;

    public ParticleSystem launchPuff;

    public float tongueSpeed = 5f;
    public float lickDuration = 0.5f;

    public bool attached = false;


    // Start is called before the first frame update
    void Start()
    {
        tongueStart = GameObject.Find("TongueStart");
        tongueEnd = GameObject.Find("TongueMaximum");
        tonguePosition = GameObject.Find("TonguePosition");
        tongueStretch = GameObject.Find("TongueStretch");
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        playerObject = GameObject.Find("Player");
        launchPuff = GameObject.Find("LaunchParticle").GetComponent<ParticleSystem>();
        gameObject.GetComponentInChildren<Renderer>().enabled = false;
        tongueStretch.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float tongueDist = Vector3.Distance(tongueStretch.transform.position, transform.position);
        tongueStretch.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, tongueDist * 3.3f);
        tongueStretch.transform.LookAt(transform.position);

        if (!attached)
        {
            transform.position = Vector3.Lerp(transform.position, tonguePosition.transform.position, tongueSpeed * Time.deltaTime);
        }

        if(attached)
        {
            player.canMove = false;
            launchPuff.Play();
            playerObject.GetComponent<Rigidbody>().useGravity = false;
            playerObject.GetComponent<Rigidbody>().isKinematic = true;
            playerObject.transform.position = Vector3.Lerp(playerObject.transform.position,transform.position,10f*Time.deltaTime);
        }

        if (Input.GetButtonDown("Fire1") && canLick)
        {
            gameObject.GetComponentInChildren<Renderer>().enabled = true;
            tongueStretch.GetComponent<Renderer>().enabled = true;
            tongueActive = true;
            player.canMove = false;
            tonguePosition.transform.position = tongueEnd.transform.position;
            StartCoroutine("Return");
            canLick = false;
        }

        if (attachedObject != null)
        {
            attachedObject.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if(tongueActive && attachedObject == null)
            {
                tonguePosition.transform.position = tongueStart.transform.position;
                attachedObject = other.gameObject;
                other.gameObject.GetComponent<Collider>().isTrigger = true;
            }
        }

        if (other.gameObject.tag == "Turnip")
        {
            if (tongueActive && attachedObject == null)
            {
                tonguePosition.transform.position = other.transform.position;
                attachedObject = other.gameObject;
                StartCoroutine("Pull");
            }
        }

        if (other.gameObject.tag == "Untagged")
        {
            tonguePosition.transform.position = tongueStart.transform.position;
        }

        if(other.gameObject.tag == "AttachPoint")
        {
            if(tongueActive)
            {
                attached = true;
                tonguePosition.transform.position = other.gameObject.transform.position;
            }

        }

        if(other.gameObject.tag == "WoodSwallow")
        {
            if (tongueActive && attachedObject == null)
            {
                tonguePosition.transform.position = tongueStart.transform.position;
                attachedObject = other.gameObject;
                player.holdingTurnip = true;
            }
        }

        if(other.gameObject.tag == "Player")
        {
            player.canMove = true;
            gameObject.GetComponentInChildren<Renderer>().enabled = false;
            tongueStretch.GetComponent<Renderer>().enabled = false;
            canLick = true; 
            if (attached)
            {
                launchPuff.Stop();
                player.canMove = true;
                playerObject.GetComponent<Rigidbody>().useGravity = true;
                playerObject.GetComponent<Rigidbody>().isKinematic = false;
                attached = false;
                tongueActive = false;
                tonguePosition.transform.position = tongueStart.transform.position;
            }

            if (attachedObject != null)
            {
                Destroy(attachedObject);
            }
        }
    }

    public IEnumerator Return()
    {
        yield return new WaitForSeconds(lickDuration);
        tonguePosition.transform.position = tongueStart.transform.position;

        tongueActive = false;
        canLick = true;

    }

    public IEnumerator Pull()
    {
        yield return new WaitForSeconds(1f);
        tonguePosition.transform.position = tongueStart.transform.position;
    }
}
