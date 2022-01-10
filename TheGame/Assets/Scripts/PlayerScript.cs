﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public bool gameStarted = true;
    public bool paused = false;
    public bool canMove = true;
    public bool damaged = false;
    public bool dead = false;
    public float speed = 10f;
    public float storedSpeed = 10f;
    public float turnipCarryingVelocity;
    public float snowBallRollVelocity;
    public float jumpSpeed = 12f;
    public bool jumpSqueeze = false;
    public bool isJumping = false;
    public float turnSmoothing = 10f;
    public bool canJump = false;
    public bool inRocketArea = false;       // boolean to disable jumping in the rocket area
    public bool canFly = false;
    public bool canGoToBed = false;
    public float coolDownTime = 0f;

    public Transform startPoint;
    public Transform spawnPoint;

    public Vector3 storedRotation;          // Variable to store the player rotation where it returns after wave action

    //Audio Variables
    private AudioSource myAudio;
    public AudioClip   landingSound;
    public AudioClip   pickupSound;

    //Variables for raycast positions
    public Transform[] rayCastPositions;
    public LayerMask groundHitLayerMask;

    //Variables for surfaces
    public bool onIce = false;

    //Variables for Physic materials
    public PhysicMaterial normalPhysics;
    public PhysicMaterial slipperyPhysics;

    // Time delay for coyote jump
    public float mayJump = 0.5f;

    // Variables for footsteps
    public bool createFootSteps;
    public GameObject footStep;
    public GameObject footStepR;
    public Transform footL;
    public Transform footR;

    // Variables for different eyes and faces
    public GameObject eyesNeutral;
    public GameObject eyesHappy;
    public GameObject mouth;

    // UI variables
    public GameObject launchButton;
    public GameObject gasTankInfo;
    public GameObject SnowBallRollPrompt;
    public GameObject goldenTurnipCollected;
    public GameObject pauseMenu;


    // Bedtime variables
    public GameObject toBedPrompt;
    public Transform[] curtains;
    public Light windowLight;
    public GameObject lightDust;

    public Vector3 movement;
    public Rigidbody myRB;
    private Animator myAnim;

    // Camera variables
    public Transform cameraTarget;
    public Transform cameraFollow;
    public Transform cameraMaximum;
    public float cameraPoint;
    public CameraScript cameraScript;
    public GameObject fadeScreen;

    public bool debugCamera = false;

    public GameObject seed;

    public float rayCheckLength = 0.4f;
    public ParticleSystem steps;
    public ParticleSystem stepPuff;
    public ParticleSystem launchPuff;

    // Variables for tongue scanning
    public GameObject[] scanners;
    public bool[] scannerDetect;
    public GameObject targetPoint;
    public float scannerLength = 100f;

    // Variables for turnip carrying and throwing
    public bool canPickTurnip = false;
    public bool holdingTurnip = false;
    public Transform turnipParent;
    public GameObject activeTurnip;
    public GameObject goldenTurnip;
    public bool sweating = false;
    public GameObject sweat;

    // Variables for snowball carrying and throwing
    public Transform snowBallPosition;
    public bool canPickSnowball = false;
    public bool holdingSnowball = false;
    public GameObject activeSnowball;

    private DialogueManager dialogueManager;
    private NPCScript npc;

    // Variables for digging mechanic
    public bool canDig = false;

    //Hitting variables
    public bool hit = false;

    // Variables for UI objects
    public Text coinsCollected;
    public Text turnipsCollected;
    public GameObject failImage;

    // Variable for gamemanager that is persistent throughout the game
    private GameManager gm;

    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        failImage.SetActive(false);
        mouth = GameObject.Find("MouthOpenAnimated");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        coinsCollected.text = "X " + gm.coins.ToString();
        turnipsCollected.text = "X " + gm.goldenTurnips.ToString();
        pauseMenu = GameObject.Find("PauseMenu");
        startPoint = GameObject.Find("StartPosition").transform;
        cameraTarget = GameObject.Find("CameraTarget").transform;
        cameraFollow = GameObject.Find("CameraFollow").transform;
        cameraMaximum = GameObject.Find("CameraMaximum").transform;
        launchPuff.Stop();
        pauseMenu.SetActive(false);
        eyesNeutral.SetActive(true);
        eyesHappy.SetActive(false);
        sweat = GameObject.Find("SweatDrops");
        sweat.SetActive(false);
        transform.position = startPoint.position;
        transform.eulerAngles = startPoint.eulerAngles;
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponentInChildren<Animator>();
        fadeScreen = GameObject.Find("FadeScreen");
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        cameraScript = Camera.main.GetComponent<CameraScript>();
        gasTankInfo = GameObject.Find("gasTankInfo");
        launchButton.SetActive(false);
        goldenTurnipCollected.SetActive(false);
        if(gasTankInfo != null)
        {
            gasTankInfo.SetActive(false);
        }
        snowBallPosition = GameObject.Find("SnowBallPosition").transform;
        snowBallRollVelocity = speed * 0.6f;
        turnipCarryingVelocity = speed * 0.5f;
        storedSpeed = speed;
        SnowBallRollPrompt.SetActive(false);
        //footL = GameObject.Find("FootStepL").transform;
        //footR = GameObject.Find("FootStepR").transform;
    }

    // Update is called once per frame
    void Update()
    {
        myAnim.SetFloat("yVelocity",myRB.velocity.y);
        mayJump -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(18);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            debugCamera = !debugCamera;
            if(debugCamera)
            {
                GameObject debugCam = GameObject.Find("Debug Camera");
                GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
                debugCam.GetComponent<Camera>().depth = 5;
                debugCam.transform.position = new Vector3(transform.position.x,transform.position.y+1f,transform.position.z-5f);
            }
            else
            {
                GameObject.Find("Debug Camera").GetComponent<Camera>().depth = -5;
                GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
            }
        }

        if (mayJump >= 0f)
        {
            canJump = true;
        }
      
        if (Input.GetButtonDown("Jump") && canJump && canMove && !holdingTurnip && !isJumping && !paused && !inRocketArea)
        {
            if(transform.parent != null)
            {
                myRB.isKinematic = false;
                myRB.useGravity = true;
                transform.parent = null;
            }
            isJumping = true;
            myAnim.SetBool("isJumping",true);
            myRB.velocity = Vector3.zero;
            myRB.AddForce(Vector3.up * jumpSpeed);
        }

        /*
        if(Input.GetButtonDown("Fire1"))
        {
            if(canPickTurnip && !holdingTurnip)
            {
                myAnim.SetBool("Pick", true);
                speed = 0f;
                canMove = false;
                StartCoroutine("PullTurnip");
            }

            else if(holdingTurnip)
            {

                speed = 0f;
                canMove = false;
                activeTurnip.transform.parent = null;
                activeTurnip.AddComponent<CapsuleCollider>();
                activeTurnip.AddComponent<Rigidbody>();
                activeTurnip.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
                holdingTurnip = false;
                canPickTurnip = true;
                //myRB.velocity = Vector3.zero;
                StartCoroutine("ThrowTurnip");
            }
        }
        */

        if(Input.GetButtonDown("Jump"))
        {
            if (canPickSnowball)
            {
                SnowBallRollPrompt.SetActive(false);
                activeSnowball.transform.position = snowBallPosition.position;
                activeSnowball.GetComponent<Rigidbody>().isKinematic = true;
                speed = snowBallRollVelocity;
            }

            if(goldenTurnipCollected.activeSelf)
            {
                canMove = true;
                goldenTurnip.SetActive(false);
                goldenTurnipCollected.SetActive(false);
                myAnim.SetBool("GoldCollected", false);
                myRB.isKinematic = false;              
                myRB.useGravity = true;
                eyesNeutral.SetActive(true);
                eyesHappy.SetActive(false);
                //mouth.SetActive(true);
                cameraScript.ReturnCamera();
                GameObject.Find("TongueBase").GetComponent<Tongue>().enabled = true;
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
            launchButton.SetActive(false);
            StartCoroutine("StartFlight");
        }

        if(Input.GetButtonDown("Fire1") && canGoToBed)
        {
            StartCoroutine("GoToBed");
            canMove = false;
        }

        if(Input.GetButtonDown("Fire2"))
        {
            StartCoroutine("Wave");
        }

        if(jumpSqueeze)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(2f, 1f, 2f), 7f * Time.deltaTime);
            if (!myAudio.isPlaying)
            {
                myAudio.PlayOneShot(landingSound);
            }
        }

        if(transform.localScale.y <= 1.1f)
        {
            jumpSqueeze = false;
        }

        if (!jumpSqueeze && transform.localScale.y != 1.5f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.5f, 1.5f, 1.5f), 10f * Time.deltaTime);
        }

        if(holdingTurnip && !IsInvoking("Sweat"))
        {
            InvokeRepeating("Sweat",0.1f,Random.Range(0.4f,0.6f));
        }

        else if(!holdingTurnip && IsInvoking("Sweat"))
        {
            CancelInvoke();
        }

        if(Input.GetButtonDown("Fire3") && !paused)
        {
            paused = true;
            pauseMenu.SetActive(true);
        }

        else if (Input.GetButtonDown("Fire3") && paused)
        {
            paused = false;
            pauseMenu.SetActive(false);
        }

        if(paused)
        {
            Time.timeScale = 0f;
        }

        else if (!paused)
        {
            Time.timeScale = 1.0f;
        }

    }

    // Legace sweating code

    /*
    public void Sweat()
    {
        int r = Random.Range(0, 4);
        for(int i=0; i< r; i++)
        {
            Instantiate(sweat, sweatEmitter[i].transform.position, sweatEmitter[i].transform.rotation);
        }
    }
    */

    public void Move(float hor, float ver)
    {
        movement.Set(hor, 0f, ver);
        movement = movement.normalized * speed * Time.deltaTime;

        if(hor == 0f && ver == 0f)
        {
            if(!onIce)
            {
                myRB.velocity = new Vector3(0f, myRB.velocity.y, 0f);
            }
            else if(onIce)
            {
                myRB.velocity = new Vector3(myRB.velocity.x, myRB.velocity.y, myRB.velocity.z);
            }

            Vector3 playerStopPoint;
            playerStopPoint = transform.position;


           cameraFollow.position = Vector3.Lerp(cameraFollow.position, playerStopPoint, 1f * Time.deltaTime);

        }

        if(hor !=0f || ver !=0f)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;
            transform.parent = null;
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

            if(coolDownTime>1f)
            {
                cameraFollow.position = Vector3.Lerp(cameraFollow.position, transform.position, 0.1f * Time.deltaTime);
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
        RaycastHit scannerHit;

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        float horSlippery = Input.GetAxis("HorizontalIce");
        float verSlippery = Input.GetAxis("VerticalIce");

        if (canMove && !damaged)
        {
            Move(hor, ver);
        }

        if(myRB.velocity.y < -0.1f && isJumping)
        {
            isJumping = false;
        }

        cameraTarget.position = new Vector3(cameraFollow.position.x, cameraPoint, cameraFollow.position.z);
        
        if ((Physics.Raycast(rayCastPositions[0].position, Vector3.down, out hit, rayCheckLength, groundHitLayerMask))
            || (Physics.Raycast(rayCastPositions[1].position, Vector3.down, out hit, rayCheckLength, groundHitLayerMask))
            || (Physics.Raycast(rayCastPositions[2].position, Vector3.down, out hit, rayCheckLength, groundHitLayerMask))
            || (Physics.Raycast(rayCastPositions[3].position, Vector3.down, out hit, rayCheckLength, groundHitLayerMask))
            || (Physics.Raycast(rayCastPositions[4].position, Vector3.down, out hit, rayCheckLength, groundHitLayerMask)))
        {
            mayJump = 0.25f;
            print("On ground");
            isJumping = false;
            cameraPoint = hit.point.y;

            if (!canJump)
            {
                myAnim.SetBool("isJumping", false);
                if (myRB.velocity.y < -2f)
                {
                    jumpSqueeze = true;
                    Instantiate(stepPuff, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                }
            }

            /*
            if(hit.transform.tag == "SandCube" && Input.GetButtonDown("Fire1"))
            {
                print("OnSand");
                hit.transform.gameObject.SetActive(false);
            }
            */
            canJump = true;
            
        }

        else
        {
            canJump = false;
        }



        for (int i = 0; i < scanners.Length; i++)
        {
            Debug.DrawRay(scanners[i].transform.position, scanners[i].transform.forward * scannerLength, Color.red);
        }


        for (int i=0; i<scanners.Length;i++)
        {          
            if (Physics.Raycast(scanners[i].transform.position, scanners[i].transform.forward, out scannerHit, scannerLength))
            {
                if (scannerHit.transform.tag == "SeaShell" || scannerHit.transform.tag == "Enemy" || scannerHit.transform.tag == "Turnip" || scannerHit.transform.tag == "AttachPoint" || scannerHit.transform.tag == "Bomb")
                {
                    scannerDetect[i] = true;
                    //print(scanners[i]+ "hit" +scannerHit.transform.gameObject.name);
                    targetPoint = scannerHit.transform.gameObject;
                }

                else
                {
                    scannerDetect[i] =false;
                    targetPoint = null;
                }
            }
        }

        /*
        if(scannerDetect[0] == false && scannerDetect[1] == false && scannerDetect[2] == false && scannerDetect[3] == false && scannerDetect[4] == false && scannerDetect[5] == false && scannerDetect[6] == false && scannerDetect[7] == false && scannerDetect[8] == false && scannerDetect[9] == false && scannerDetect[10] == false && scannerDetect[11] == false && scannerDetect[12] == false && scannerDetect[13] == false && scannerDetect[14] == false)
        {
            targetPoint = null;
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ice")
        {

        }

        if(other.gameObject.tag == "Pickup")
        {
            myAudio.PlayOneShot(pickupSound);
        }

        if(other.gameObject.tag == "SpawnPoint")
        {
            spawnPoint = other.gameObject.transform;
        }

        if(other.gameObject.tag == "DeathZone" || other.gameObject.tag == "Water" || other.gameObject.tag == "Hazard")
        {
            dead = true;
            canMove = false;
            StartCoroutine("Fail");
        }

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

        if(other.gameObject.tag == "Golden Turnip")
        {
            if(!other.GetComponent<CollectableGoldenTurnip>().collected)
            {
                GameObject.Find("TongueBase").GetComponent<Tongue>().enabled = false;
                canMove = false;
                goldenTurnip = other.gameObject;
                if (goldenTurnip != null)
                {
                    goldenTurnip.SetActive(true);
                }

                steps.Stop();
                eyesNeutral.SetActive(false);
                eyesHappy.SetActive(true);
                if(!mouth.activeSelf)
                {
                    mouth.SetActive(false);
                }               
                myAnim.SetBool("GoldCollected", true);
                
                cameraScript.DialogueCamera();
                myRB.velocity = Vector3.zero;
                myRB.useGravity = false;
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
                other.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z);
                other.gameObject.GetComponentInChildren<SpinningObject>().speed = 0f;
                other.gameObject.transform.eulerAngles = new Vector3(0f, 90f, 0f);
                goldenTurnipCollected.SetActive(true);
                gm.GoldenTurnipCollected();
                other.gameObject.GetComponent<CollectableGoldenTurnip>().collected = true;

                if (gm.levelInfo.levelNumber == 1)
                {
                    gm.goldenTurnipsGrassLands += 1;
                }

                if (gm.levelInfo.levelNumber == 2)
                {
                    gm.goldenTurnipsDesert += 1;
                }

                if (gm.levelInfo.levelNumber == 3)
                {
                    gm.goldenTurnipsSnowland += 1;
                }

                if (gm.levelInfo.levelNumber == 4)
                {
                    gm.goldenTurnipsBlockTown += 1;
                }

                if (gm.levelInfo.levelNumber == 5)
                {
                    gm.goldenTurnipsForest += 1;
                }

                if (gm.levelInfo.levelNumber == 6)
                {
                    gm.goldenTurnipsMoon += 1;
                }

                if (gm.levelInfo.levelNumber == 11)
                {
                    gm.goldenTurnipsIce += 1;
                }
            }
        }


        if (other.gameObject.name == "ForestArea")
        {
            cameraScript.OverHeadCamera();
        }

        if(other.gameObject.name == "DoorOutToIn")
        {
            StartCoroutine(ChangeLevel(9));
        }

        if (other.gameObject.name == "DoorInToOut")
        {
            StartCoroutine(ChangeLevel(17));
            dialogueManager.ReturnLevel();
        }

        if (other.gameObject.name == "DoorToSpace")
        {
            StartCoroutine(ChangeLevel(6));
            dialogueManager.ReturnLevel();
        }

        if (other.gameObject.name == "DoorToHomeUpStairs")
        {
            StartCoroutine(ChangeLevel(7));
        }

        if(other.gameObject.name == "DoorToDownStairs")
        {
            StartCoroutine(ChangeLevel(9));
        }

        if (other.gameObject.name == "DoorToFirePlace")
        {
            StartCoroutine(ChangeLevel(3));
        }

        if(other.gameObject.name == "SpaceShipTrigger")
        {
            inRocketArea = true;
            launchButton.SetActive(true);
            canJump = false;
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
        if(other.gameObject.tag == "Ice")
        {
            onIce = true;
            //GetComponent<Collider>().material = slipperyPhysics;
        }

        if (other.gameObject.tag != "Ice")
        {
            onIce = false;
            //GetComponent<Collider>().material = normalPhysics;
        }

        if (other.gameObject.tag == "Sand")
        {
            createFootSteps = true;
        }

        if(other.gameObject.tag == "Damage" || other.gameObject.tag == "LargeDamage" || other.gameObject.tag == "Enemy")
        {
            if(!hit)
            {
                StartCoroutine("Damage");
            }           
        }

    }

    public void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "MovingPlatform" && movement == new Vector3(0f, 0f, 0f) && !isJumping)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().useGravity = false;
            transform.parent = other.transform;
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
            canPickTurnip = false;
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

        if (other.gameObject.name == "SpaceShipTrigger")
        {
            canJump = false;
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
            inRocketArea = false;
            launchButton.SetActive(false);
            canFly = false;
            canJump = true;
        }

        if (other.gameObject.name == "ToBed")
        {
            toBedPrompt.SetActive(false);
            canGoToBed = false;
        }

        if (other.gameObject.tag == "DialogueTrigger")
        {
            canPickTurnip = true;
        }

    }

    public IEnumerator PullTurnip()
    {
        yield return new WaitForSeconds(1.1f);
        activeTurnip.transform.parent = transform;
        activeTurnip.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        activeTurnip.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        activeTurnip.transform.eulerAngles = new Vector3(90f, 90f, 180f);
        canPickTurnip = false;
        holdingTurnip = true;
        yield return new WaitForSeconds(0.5f);
        canMove = true;
        speed = turnipCarryingVelocity;
    }

    public IEnumerator ThrowTurnip()
    {
        myAnim.SetTrigger("Throw");
        yield return new WaitForSeconds(0.5f);
        speed = storedSpeed;
        canMove = true;
        myRB.velocity = Vector3.zero;
        myAnim.SetBool("Pick", false);
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

    public IEnumerator Fade()
    {
        canMove = false;
        myAnim.SetBool("isRunning", false);
        steps.Stop();
        fadeScreen.GetComponent<Animator>().SetTrigger("ChangeLevel");
        yield return new WaitForSeconds(1);
        canMove = true;
    }


    // Old. Check if it can be deleted

    /*
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
    */

    public IEnumerator StartFlight()
    {
        canMove = false;
       
        eyesNeutral.SetActive(false);
        eyesHappy.SetActive(true);
        transform.eulerAngles = new Vector3(0f, -180f, 0f);
        myAnim.SetTrigger("Wave");
        yield return new WaitForSeconds(1f);
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        yield return new WaitForSeconds(1f);
        gm.ActivateFly();
        //canFly = false;
        launchButton.SetActive(false);
        gameObject.SetActive(false);
    }

    public IEnumerator Fail()
    {
        GameObject tongue = GameObject.Find("TongueBase");
        tongue.GetComponent<Tongue>().enabled = false;
        tongue.transform.position = transform.position;
        yield return new WaitForSeconds(1f);
        failImage.SetActive(true);
        yield return new WaitForSeconds(1f);

        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
            myRB.velocity = Vector3.zero;
        }
        else
        {
            transform.position = startPoint.position;
        }

        yield return new WaitForSeconds(0.5f);
        failImage.SetActive(false);
        tongue.GetComponent<Tongue>().enabled = true;
        dead = false;
        yield return new WaitForSeconds(0.5f);
        canMove = true;
    }

    public IEnumerator Wave()
    {
        canMove = false;
        eyesNeutral.SetActive(false);
        eyesHappy.SetActive(true);
        storedRotation = transform.eulerAngles;
        transform.eulerAngles = new Vector3(0f, -180f, 0f);
        myAnim.SetTrigger("Wave");
        yield return new WaitForSeconds(1f);        
        eyesNeutral.SetActive(true);
        eyesHappy.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        canMove = true;
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

    public void LaunchEffectStart()
    {
        launchPuff.Play();
    }

    public void LaunchEffectStop()
    {
        launchPuff.Stop();
    }

    public IEnumerator Damage()
    {

        myAnim.SetBool("isJumping", true);
        damaged = true;
        hit = true;
        canMove = false;
        canJump = false;
        sweat.SetActive(true);
        myRB.velocity = Vector3.zero;
        myRB.AddForce(transform.forward * -300f);
        myRB.AddForce(transform.up * 300f);
        yield return new WaitForSeconds(0.5f);
        sweat.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        canMove = true;
        canJump = true;
        hit = false;
        damaged = false;       
        print("Damage");
    }
}
