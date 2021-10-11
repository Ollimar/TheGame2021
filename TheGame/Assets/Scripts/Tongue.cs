﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    public bool tongueActive = false;

    private PlayerScript player;
    public GameObject playerObject;

    public GameObject tongueStart;
    public GameObject tongueEnd;

    public GameObject tongueStretch;

    public GameObject mouth;

    public GameObject tonguePosition;
    public GameObject attachedObject;

    public GameObject coin;

    public ParticleSystem launchPuff;

    public float tongueSpeed = 5f;
    public float lickDuration = 0.5f;

    public bool attached = false;

    public bool carryingBomb = false;
    public GameObject bomb;

    public ParticleSystem eatParticle;

    private AudioSource myAudio;
    public AudioClip pullSound;
    public AudioClip tongueSound;
    public AudioClip swallowSound;


    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        mouth = GameObject.Find("MouthOpenAnimated");
        mouth.GetComponent<Animator>().enabled = false;
        mouth.SetActive(false);
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
            if(!player.dead)
            {
                playerObject.transform.position = Vector3.Lerp(playerObject.transform.position, transform.position, 10f * Time.deltaTime);
            }
            else
            {
                return;
            }
        }

        if (Input.GetButtonDown("Fire1") && attachedObject == null && !tongueActive && !carryingBomb)
        {
            if(player.targetPoint != null)
            {
                playerObject.transform.LookAt(player.targetPoint.transform);
            }
            myAudio.pitch = Random.Range(0.9f,1.1f); 
            myAudio.PlayOneShot(tongueSound);
            mouth.SetActive(true);
            mouth.GetComponent<Animator>().enabled = true;
            mouth.GetComponent<Animator>().SetBool("Open",true);
            playerObject.GetComponentInChildren<Animator>().SetBool("Eat", true);
            player.steps.Stop();
            player.myRB.velocity = new Vector3(0f, player.myRB.velocity.y, 0f);
            gameObject.GetComponentInChildren<Renderer>().enabled = true;
            tongueStretch.GetComponent<Renderer>().enabled = true;
            tongueActive = true;
            player.canMove = false;
            tonguePosition.transform.position = tongueEnd.transform.position;
            StartCoroutine("Return");           
        }

        if (attachedObject != null)
        {
            if(attachedObject.tag == "PullObject")
            {
                print("PullObject");
                transform.position = attachedObject.transform.position;
            }
            else
            {
                attachedObject.transform.position = transform.position;
            }
        }

        if(Input.GetButtonDown("Fire1") && carryingBomb)
        {
            attachedObject = null;
            bomb.transform.parent = null;
            bomb.transform.position = tongueStart.transform.position;
            bomb.transform.localScale = new Vector3(2.6852f, 2.6852f, 2.6852f);
            bomb.GetComponent<Collider>().isTrigger = false;
            bomb.GetComponent<Rigidbody>().useGravity = true;
            bomb.GetComponent<Rigidbody>().velocity = Vector3.zero;
            bomb.GetComponent<Rigidbody>().AddForce(playerObject.transform.forward * 1000f);
            carryingBomb = false;
            bomb = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if(tongueActive && attachedObject == null)
            {
                tonguePosition.transform.position = other.transform.position;
                attachedObject = other.gameObject;
                other.GetComponent<Enemy>().caught = true;
                other.gameObject.GetComponent<Collider>().isTrigger = true;
                if(other.GetComponent<Animator>())
                {
                    other.GetComponent<Animator>().SetBool("EnemyEaten", true);
                }
                StartCoroutine("Pull");
            }
        }

        if (other.gameObject.tag == "Bomb")
        {
            if (tongueActive && attachedObject == null)
            {
                bomb = other.gameObject;
                carryingBomb = true;
                tonguePosition.transform.position = other.transform.position;
                attachedObject = other.gameObject;
                other.gameObject.GetComponent<Collider>().isTrigger = true;
                StartCoroutine("Pull");
            }
        }

        if (other.gameObject.tag == "Turnip")
        {
            if (tongueActive && attachedObject == null)
            {
                // Instantiate coin from picked object
                int rnd;
                rnd = Random.Range(0, 5);
                if(rnd >= 3)
                {
                    GameObject newCoin = Instantiate(coin, new Vector3(other.transform.position.x, other.transform.position.y + 1f, other.transform.position.z), transform.rotation);
                    newCoin.GetComponent<Rigidbody>().AddForce(Vector3.up * 400f);
                }

                tonguePosition.transform.position = other.transform.position;
                attachedObject = other.gameObject;
                if(other.GetComponent<Rigidbody>())
                {
                    other.GetComponent<Rigidbody>().isKinematic = false;
                    other.GetComponent<Rigidbody>().useGravity = true;
                }
                if (other.GetComponent<Collider>())
                {
                    other.GetComponent<Collider>().isTrigger = true;
                }
                StartCoroutine("Pull");
            }
        }

        if (other.gameObject.tag == "SeaShell")
        {
            if (tongueActive && attachedObject == null)
            {
                tonguePosition.transform.position = other.transform.position;
                tonguePosition.transform.position = tongueStart.transform.position;
                attachedObject = other.gameObject;
                if(other.GetComponent<Rigidbody>())
                {
                    other.GetComponent<Rigidbody>().isKinematic = false;
                    other.GetComponent<Rigidbody>().useGravity = true;
                }
            }
        }

        if (other.gameObject.tag == "PullObject")
        {
            if(other.gameObject.GetComponent<PullObject>().canPull)
            {
                if (tongueActive && attachedObject == null)
                {
                    tonguePosition.transform.position = tongueStart.transform.position;
                    attachedObject = other.gameObject;

                    if (!player.IsInvoking("Sweat"))
                    {
                        player.InvokeRepeating("Sweat", 0.1f, Random.Range(0.4f, 0.6f));
                    }

                    other.gameObject.GetComponent<PullObject>().StartCoroutine("StartPull");
                    //StartCoroutine("Return");
                    //StartCoroutine("Pull");
                }
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
                tongueActive = false;
            }
        }

        if(other.gameObject.tag == "Player")
        {
            playerObject.GetComponentInChildren<Animator>().SetBool("Eat", false);
            player.canMove = true;
            gameObject.GetComponentInChildren<Renderer>().enabled = false;
            tongueStretch.GetComponent<Renderer>().enabled = false;
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
                myAudio.PlayOneShot(swallowSound);
                if (attachedObject.tag == "Enemy")
                {
                    attachedObject.GetComponent<Enemy>().puff.transform.parent = null;
                    attachedObject.GetComponent<Enemy>().eaten = true;
                    attachedObject = null;
                }
                if(attachedObject.tag == "Bomb")
                {
                    attachedObject.transform.parent = transform;
                    attachedObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                }
                else if(attachedObject.tag == "PullObject")
                {
                    return;
                }
                else
                {
                    Destroy(attachedObject);
                    attachedObject = null;
                }                
            }
            mouth.GetComponent<Animator>().SetBool("Open", false);
            //StartCoroutine("MouthShut");
        }
    }

    public IEnumerator Return()
    {
        yield return new WaitForSeconds(lickDuration);
        tonguePosition.transform.position = tongueStart.transform.position;
        
        tongueActive = false;
    }

    public IEnumerator MouthShut()
    {
        yield return new WaitForSeconds(0.25f);
        mouth.SetActive(false);
    }

    public IEnumerator Pull()
    {
        yield return new WaitForSeconds(0.1f);
        if(attachedObject != null && attachedObject.tag == "Enemy")
        {
            attachedObject.GetComponent<Enemy>().puff.Play();
        }
        tonguePosition.transform.position = tongueStart.transform.position;
        if(player.IsInvoking("Sweat"))
        {
            CancelInvoke();
        }
    }
}
