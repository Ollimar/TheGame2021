﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public bool canMove = true;
    public float speed = 10f;
    public float storedSpeed = 10f;
    public float snowBallRollVelocity;
    public float jumpSpeed = 12f;
    public float turnSmoothing = 10f;
    public bool canJump = false;
    public bool canFly = false;
    public bool canGoToBed = false;
    public float coolDownTime = 0f;

    // Variables for footsteps
    public bool createFootSteps;
    public GameObject footStep;
    public GameObject footStepR;
    public Transform footL;
    public Transform footR;

    public GameObject launchButton;
    public GameObject gasTankInfo;
    public GameObject SnowBallRollPrompt;

    // Bedtime variables
    public GameObject toBedPrompt;
    public Transform[] curtains;
    public Light windowLight;
    public GameObject lightDust;

    private Vector3 movement;
    private Rigidbody myRB;
    private Animator myAnim;

    // Camera variables
    public Transform cameraTarget;
    public float cameraPoint;
    public CameraScript cameraScript;
    public GameObject fadeScreen;

    public GameObject seed;

    public float rayCheckLength = 0.4f;
    public ParticleSystem steps;
    public ParticleSystem stepPuff;

    // Variables for turnip carrying and throwing
    public bool canPickTurnip = false;
    public bool holdingTurnip = false;
    public GameObject activeTurnip;

    // Variables for snowball carrying and throwing
    public Transform snowBallPosition;
    public bool canPickSnowball = false;
    public bool holdingSnowball = false;
    public GameObject activeSnowball;

    private DialogueManager dialogueManager;
    private NPCScript npc;

    // Variables for digging mechanic
    public bool canDig = false;

    // Variable for gamemanager that is persistent throughout the game
    private GameManager gm;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(gm.levelInfo.levelNumber == 0)
        {
            toBedPrompt.SetActive(false);
        }
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        fadeScreen = GameObject.Find("FadeScreen");
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        cameraScript = Camera.main.GetComponent<CameraScript>();
        gasTankInfo = GameObject.Find("gasTankInfo");
        launchButton.SetActive(false);
        gasTankInfo.SetActive(false);
        snowBallPosition = GameObject.Find("SnowBallPosition").transform;
        snowBallRollVelocity = speed * 0.6f;
        storedSpeed = speed;
        SnowBallRollPrompt.SetActive(false);
        footL = GameObject.Find("FootStepL").transform;
        footR = GameObject.Find("FootStepR").transform;
    }

    // Update is called once per frame
    void Update()
    {
        myAnim.SetFloat("yVelocity",myRB.velocity.y);

        /*  In case you need to jump
         
        if (Input.GetButtonDown("Jump") && canJump && canMove)
        {
            myAnim.SetBool("isJumping",true);
            myRB.AddForce(Vector3.up * jumpSpeed);
        }
        */

        if(Input.GetButtonDown("Fire1"))
        {
            if(canPickTurnip && !holdingTurnip)
            {
                activeTurnip.transform.parent = transform;
                activeTurnip.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
                canPickTurnip = false;
                holdingTurnip = true;
            }

            else if(holdingTurnip)
            {
                activeTurnip.transform.parent = null;
                activeTurnip.AddComponent<CapsuleCollider>();
                activeTurnip.AddComponent<Rigidbody>();
                activeTurnip.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
                holdingTurnip = false;
                canPickTurnip = true;
            }

        }

        if(Input.GetButton("Fire1"))
        {
            if (canPickSnowball)
            {
                SnowBallRollPrompt.SetActive(false);
                activeSnowball.transform.position = snowBallPosition.position;
                speed = snowBallRollVelocity;
            }
        }

        if(Input.GetButtonUp("Fire1") && canPickSnowball)
        {
            activeSnowball.GetComponent<SnowballScript>().rolling = false;
            speed = storedSpeed;
            //SnowBallRollPrompt.SetActive(true);
        }

        if(Input.GetButtonDown("Jump") && canFly)
        {
            StartFlight();
        }

        if(Input.GetButtonDown("Fire1") && canGoToBed)
        {
            StartCoroutine("GoToBed");
            canMove = false;
        }
    }

    public void Move(float hor, float ver)
    {

        movement.Set(hor, 0f, ver);
        movement = movement.normalized * speed * Time.deltaTime;

        if(hor == 0f && ver == 0f)
        {
            myRB.velocity = new Vector3(0f,myRB.velocity.y,0f);
        }

        if(hor !=0f || ver !=0f)
        {
            coolDownTime += Time.deltaTime;
            if (steps.isStopped && canJump)
            {
                steps.Play();
            }
            Rotating(hor, ver);
            myAnim.SetBool("isRunning", true);

            if(activeSnowball != null)
            {
                activeSnowball.GetComponent<SnowballScript>().rolling = true;
            }
        }
        else
        {
            coolDownTime = 0f;
            steps.Stop();
            myAnim.SetBool("isRunning", false);

            if (activeSnowball != null)
            {
                activeSnowball.GetComponent<SnowballScript>().rolling = false;
            }
        }

        if(!canJump)
        {
            steps.Stop();
        }

        if(coolDownTime>0.1f)
        {
            myRB.MovePosition(transform.position + movement);
        }

    }

    public void Rotating(float hor, float ver)
    {
        Vector3 targetDirection = new Vector3(hor, 0f, ver);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(myRB.rotation, targetRotation, turnSmoothing * Time.deltaTime);
        myRB.MoveRotation(newRotation);
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");


        if(canMove)
        {
            Move(hor, ver);
        }

        cameraTarget.position = new Vector3(transform.position.x, cameraPoint, transform.position.z);

        if(Physics.Raycast(transform.position,Vector3.down,out hit, rayCheckLength))
        {

            cameraPoint = hit.point.y;

            if (!canJump)
            {
                myAnim.SetBool("isJumping", false);
                if (myRB.velocity.y < -2f)
                {
                    Instantiate(stepPuff, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                }
            }

            if(hit.transform.tag == "Wheel")
            {
                transform.parent = hit.transform;
            }
            else
            {
                transform.parent = null;
            }

            if(hit.transform.tag == "SandCube" && Input.GetButtonDown("Fire1"))
            {
                print("OnSand");
                hit.transform.gameObject.SetActive(false);
                canDig = true;
            }

            canJump = true;
        }

        else
        {
            canJump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Turnip" && !holdingTurnip)
        {
            canPickTurnip = true;
            activeTurnip = other.gameObject;
        }

        if (other.gameObject.tag == "Snowball")
        {
            if(activeSnowball == null)
            {
                canPickSnowball = true;
                activeSnowball = other.transform.parent.gameObject;
                SnowBallRollPrompt.SetActive(true);
            }
        }

        if (other.gameObject.tag == "GasTank")
        {
            gm.SpaceShipStaminaUpdate();
            canMove = false;
            steps.Stop();
            myAnim.SetBool("isRunning",false);
            gasTankInfo.SetActive(true);
            Destroy(other.gameObject);
        }

        if (other.gameObject.name == "ForestArea")
        {
            cameraScript.OverHeadCamera();
        }

        if(other.gameObject.name == "DoorOutToIn")
        {
            StartCoroutine(ChangeLevel(2));
        }

        if (other.gameObject.name == "DoorInToOut")
        {
            StartCoroutine(ChangeLevel(7));
            dialogueManager.ReturnLevel();
        }

        if (other.gameObject.name == "DoorToHomeUpStairs")
        {
            StartCoroutine(ChangeLevel(0));
        }

        if(other.gameObject.name == "DoorToDownStairs")
        {
            StartCoroutine(ChangeLevel(2));
        }

        if (other.gameObject.name == "DoorToFirePlace")
        {
            StartCoroutine(ChangeLevel(3));
        }

        if(other.gameObject.name == "SpaceShipTrigger")
        {
            launchButton.SetActive(true);
            canFly = true;
        }

        if(other.gameObject.name == "HiPowerEngine")
        {
            GameObject.Find("SpaceShip").GetComponent<SpaceShipScript>().stage2 = true;
        }

        if(other.gameObject.tag == "SkyBooster")
        {
            gm.SkyActivated();
        }

        if(other.gameObject.tag == "Key")
        {
            other.gameObject.GetComponent<KeyScript>().Open();
            Destroy(other.gameObject);
        }

        if (other.gameObject.name == "ToBed")
        {
            toBedPrompt.SetActive(true);
            canGoToBed = true;
        }

    }

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Sand")
        {
            createFootSteps = true;
        }
    }

    public void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Sand")
        {
            createFootSteps = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Turnip" && !holdingTurnip)
        {
            canPickTurnip = true;
            activeTurnip = other.gameObject;
        }

        if (other.gameObject.tag == "DialogueTrigger")
        {
            npc = other.gameObject.GetComponent<NPCScript>();
            if (Input.GetButtonDown("Fire1") && !dialogueManager.dialogueWindow.activeSelf)
            {
                other.gameObject.GetComponent<NPCScript>().dialogueIndicator.SetActive(false);
                dialogueManager.dialogueWindow.SetActive(true);
                npc.SetDialogue();
                canMove = false;
                cameraScript.DialogueCamera();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Turnip" && !holdingTurnip)
        {
            canPickTurnip = false;
            activeTurnip = null;
        }

        if (other.gameObject.tag == "Snowball")
        {
            canPickSnowball = false;
            activeSnowball = null;
            speed = storedSpeed;
            SnowBallRollPrompt.SetActive(false);
            if (activeSnowball != null)
            {
                activeSnowball.GetComponent<SnowballScript>().rolling = false;
            }
        }

        if (other.gameObject.name == "ForestArea")
        {
            cameraScript.ReturnCamera();
        }

        if (other.gameObject.name == "SpaceShipTrigger")
        {
            launchButton.SetActive(false);
            canFly = false;
        }

        if (other.gameObject.name == "ToBed")
        {
            toBedPrompt.SetActive(false);
            canGoToBed = false;
        }
    }

    public IEnumerator ChangeLevel(int levelNumber)
    {
        canMove = false;
        myAnim.SetBool("isRunning", false);
        steps.Stop();
        fadeScreen.GetComponent<Animator>().SetTrigger("ChangeLevel");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelNumber);
    }

    public IEnumerator GoToBed()
    {
        canMove = false;
        myAnim.SetBool("isRunning", false);
        curtains[0].GetComponent<Animator>().SetTrigger("CurtainClose");
        curtains[1].GetComponent<Animator>().SetTrigger("CurtainClose");
        windowLight.enabled = false;
        lightDust.SetActive(false);
        yield return new WaitForSeconds(1f);
        fadeScreen.GetComponent<Animator>().SetTrigger("ChangeLevel");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Test2D");
    }

    public void StartFlight()
    {
        gm.ActivateFly();
        canFly = false;
        launchButton.SetActive(false);
        gameObject.SetActive(false);
    }

    public void FootStepL()
    {
        if(createFootSteps)
        {
            Instantiate(footStep, footL.position, transform.rotation);
        }
    }

    public void FootStepR()
    {
        if(createFootSteps)
        {
            Instantiate(footStepR, footR.position, transform.rotation);
        }
    }
}
