using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    public bool tongueActive = false;

    private PlayerScript player;
    public GameObject playerObject;

    public GameObject tongueStart;
    public GameObject tongueEnd;

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
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        playerObject = GameObject.Find("Player");
        launchPuff = GameObject.Find("LaunchParticle").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!attached)
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

        if (Input.GetButtonDown("Fire1"))
        {
            tongueActive = true;
            player.canMove = false;
            tonguePosition.transform.position = tongueEnd.transform.position;
            StartCoroutine("Return");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            tonguePosition.transform.position = tongueStart.transform.position;
            attachedObject = other.gameObject;
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Untagged")
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

        if(other.gameObject.tag == "Player")
        {
            if(attached)
            {
                launchPuff.Stop();
                player.canMove = true;
                playerObject.GetComponent<Rigidbody>().useGravity = true;
                playerObject.GetComponent<Rigidbody>().isKinematic = false;
                attached = false;
                tongueActive = false;
                tonguePosition.transform.position = tongueStart.transform.position;
            }
        }
    }

    public IEnumerator Return()
    {
        yield return new WaitForSeconds(lickDuration);
        tonguePosition.transform.position = tongueStart.transform.position;
        player.canMove = true;
        tongueActive = false;
        if (attachedObject != null)
        {
            Destroy(attachedObject);
        }
    }
}
