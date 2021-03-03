using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    public bool canMove = true;
    public float speed = 10f;
    public float storedSpeed = 10f;
    public float turnipCarryingVelocity;
    public float snowBallRollVelocity;
    public float jumpSpeed = 12f;
    public bool jumpSqueeze = false;
    public float turnSmoothing = 10f;
    public bool canJump = false;
    public bool canFly = false;
    public bool canGoToBed = false;
    public float coolDownTime = 0f;

    public Transform startPoint;
    public Transform spawnPoint;

    // Variables for footsteps
    public bool createFootSteps;
    public GameObject footStep;
    public GameObject footStepR;
    public Transform footL;
    public Transform footR;

    // Variables for different eyes
    public GameObject eyesNeutral;
    public GameObject eyesHappy;


    // UI variables
    public GameObject launchButton;
    public GameObject gasTankInfo;
    public GameObject SnowBallRollPrompt;
    public GameObject goldenTurnipCollected;


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
    public GameObject goldenTurnip;

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
        startPoint = GameObject.Find("StartPosition").transform;
        eyesNeutral.SetActive(true);
        eyesHappy.SetActive(false);
        transform.position = startPoint.position;
        transform.eulerAngles = startPoint.eulerAngles;

        if (gm.levelInfo.levelNumber == 0)
        {
            toBedPrompt.SetActive(false);
        }

        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponentInChildren<Animator>();
        fadeScreen = GameObject.Find("FadeScreen");
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        cameraScript = Camera.main.GetComponent<CameraScript>();
        gasTankInfo = GameObject.Find("gasTankInfo");
        launchButton.SetActive(false);
        goldenTurnipCollected.SetActive(false);
        gasTankInfo.SetActive(false);
        snowBallPosition = GameObject.Find("SnowBallPosition").transform;
        snowBallRollVelocity = speed * 0.6f;
        turnipCarryingVelocity = speed * 0.5f;
        storedSpeed = speed;
        SnowBallRollPrompt.SetActive(false);
        footL = GameObject.Find("FootStepL").transform;
        footR = GameObject.Find("FootStepR").transform;
    }

    // Update is called once per frame
    void Update()
    {
        myAnim.SetFloat("yVelocity",myRB.velocity.y);
      
        if (Input.GetButtonDown("Jump") && canJump && canMove && !holdingTurnip)
        {
            myAnim.SetBool("isJumping",true);
            myRB.AddForce(Vector3.up * jumpSpeed);
        }


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

        if(Input.GetButton("Fire1"))
        {
            if (canPickSnowball)
            {
                SnowBallRollPrompt.SetActive(false);
                activeSnowball.transform.position = snowBallPosition.position;
                speed = snowBallRollVelocity;
            }

            if(goldenTurnipCollected.activeSelf)
            {
                canMove = true;
                goldenTurnipCollected.SetActive(false);
                myAnim.SetBool("GoldCollected", false);
                myRB.useGravity = false;
                goldenTurnip.SetActive(false);
                myRB.useGravity = true;
                eyesNeutral.SetActive(true);
                eyesHappy.SetActive(false);
                cameraScript.ReturnCamera();
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

        if(jumpSqueeze)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(2f, 1f, 2f), 7f * Time.deltaTime);
        }

        if(transform.localScale.y <= 1.1f)
        {
            jumpSqueeze = false;

        }

        if (!jumpSqueeze && transform.localScale.y != 1.5f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.5f, 1.5f, 1.5f), 10f * Time.deltaTime);
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
                    jumpSqueeze = true;
                    Instantiate(stepPuff, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                }
            }

            if(hit.transform.tag == "Wheel")
            {
                transform.position = transform.position + hit.transform.position;
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
        if(other.gameObject.tag == "SpawnPoint")
        {
            spawnPoint = other.gameObject.transform;
        }

        if(other.gameObject.tag == "DeathZone")
        {
            if(spawnPoint !=null)
            {
                transform.position = spawnPoint.position;
                myRB.velocity = Vector3.zero;
            }
            else
            {
                transform.position = startPoint.position;
            }

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
            canMove = false;
            if(goldenTurnip !=null)
            {
                goldenTurnip.SetActive(true);
            }
            steps.Stop();
            eyesNeutral.SetActive(false);
            eyesHappy.SetActive(true);
            myAnim.SetBool("GoldCollected",true);
            goldenTurnip = other.gameObject;
            cameraScript.DialogueCamera();
            myRB.velocity = Vector3.zero;
            myRB.useGravity = false;
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            other.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z);
            other.gameObject.GetComponentInChildren<SpinningObject>().speed = 0f;
            other.gameObject.transform.eulerAngles = new Vector3(0f, 90f, 0f);
            goldenTurnipCollected.SetActive(true);
            gm.goldenTurnips += 1;
            if(gm.levelInfo.levelNumber == 1)
            {
                gm.goldenTurnipsGrassLands += 1;
            }

            if (gm.levelInfo.levelNumber == 2)
            {
                gm.goldenTurnipsDesert += 1;
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
