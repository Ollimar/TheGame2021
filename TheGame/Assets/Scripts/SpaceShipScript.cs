using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpaceShipScript : MonoBehaviour
{
    public float speed = 5f;
    public float normalSpeed = 15f;
    public float floatingSpeed = 7f;
    public float turnSmoothing = 15f;

    private Vector3 movement;
    private Rigidbody myRB;
    private Animator myAnim;

    public float launchTimer;
    public float launchPower = 600f;
    public ParticleSystem launchParticles;
    public GameObject[] trails;

    public float stamina = 100f;
    public float fullStamina = 10f;
    public float staminaMultiplier; //this number decreases as player collects more gas tanks

    public bool activate    = false;
    public bool canLaunch   = true;
    public bool canMove     = false;
    public bool canLand     = false;
    public bool falling     = false;

    // Variables for allowing player to move between different "heights"
    public bool boosterSky = false;
    public bool boosterSpace = false;

    //Which levels player has activated
    public bool stage1 = false;
    public bool stage2 = false;

    public Transform landingTarget;

    public GameObject landButton;

    public Image spaceShipStaminaImage;
    public GameObject parachute;

    public GameObject fadeScreen;

    private GameManager gm;
    private LevelInfo levelInfo;

    // Start is called before the first frame update
    void Start()
    {
        gm                  = GameObject.Find("GameManager").GetComponent<GameManager>();
        myRB                = GetComponent<Rigidbody>();
        myAnim              = GetComponent<Animator>();
        parachute           = GameObject.Find("Parachute");
        levelInfo           = GameObject.Find("LevelInfo").GetComponent<LevelInfo>();
        myAnim.enabled      = false;
        landButton.SetActive(false);
        launchParticles.Stop();
        spaceShipStaminaImage.transform.parent.gameObject.SetActive(false);
        fadeScreen          = GameObject.Find("FadeScreen");
        boosterSky          = gm.boosterSky;
        boosterSpace        = gm.boosterSpace;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && activate && !falling && canLaunch && !myAnim.enabled)
        {
           canMove = false;
           transform.eulerAngles = new Vector3(-90f, 0f, 0f);
           myAnim.enabled = true;
           myAnim.SetTrigger("Air");
           launchParticles.Play();

           for (int i = 0; i < trails.Length; i++)
           {
               trails[i].SetActive(true);
           }
        }

        if (Input.GetButtonUp("Jump"))
        {
            //myAnim.enabled = false;
            launchTimer = 0f;
        }

        if(Input.GetButton("Jump") && canLand)
        {
            Landing();
        }

        if (falling)
        {
            speed = floatingSpeed;
            for(int i=0; i<trails.Length;i++)
            {
                trails[i].SetActive(false);
            }
            myRB.useGravity = true;
            myRB.isKinematic = false;
            myRB.drag = 5f;
            transform.eulerAngles = new Vector3(-90f, 0f, 0f);
            parachute.SetActive(true);
            parachute.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            if(transform.position.y <= -50 && levelInfo.levelNumber == 1)
            {
                stage1 = false;
                landingTarget = GameObject.Find("CubeHome").transform.GetChild(0);
                Landing();
            }

            if (transform.position.y <= 75f && levelInfo.levelNumber == 4)
            {
                StartCoroutine(ChangeLevel(1));
            }
        }

        /*
        else if (transform.position.y >= 8f && !stage1)
        {
            Stop();
            StartCoroutine("Relaunch");
        }
        */

        if (transform.position.y >= 150f && levelInfo.levelNumber == 1)
        {
            StartCoroutine(ChangeLevel(6));
        }


        if (canMove)
        {
            stamina -= Time.deltaTime* staminaMultiplier;
            spaceShipStaminaImage.fillAmount = stamina/10;
        }

        if(stamina <= 0)
        {
            falling = true;
        }
    }

    private void FixedUpdate()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        if(canMove)
        {
            Move(hor, ver);
        }
    }

    public void Move(float hor, float ver)
    {
        movement.Set(hor, 0f, ver);
        movement = movement.normalized * speed * Time.deltaTime;
        myRB.MovePosition(transform.position + movement);

        if(hor !=0f && !falling || ver != 0f && !falling)
        {
            Rotating(hor,ver);
            launchParticles.Play();
        }

        else
        {
            launchParticles.Stop();
        }
    }

    public void Rotating(float hor, float ver)
    {
        Vector3 targetDirection = new Vector3(hor, 0f, ver);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(myRB.rotation, targetRotation, turnSmoothing * Time.deltaTime);
        myRB.MoveRotation(newRotation);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "LandingPlatform")
        {
            landingTarget = other.GetComponent<LanderAreaScript>().landingSpot;
            canLand = true;
            landButton.SetActive(true);
            landButton.GetComponentInChildren<Text>().text = "Land to " + other.gameObject.GetComponent<LanderAreaScript>().planetName;
        }

        else if(other.gameObject.tag == "Lander")
        {
            if(falling)
            {
                landingTarget = other.GetComponent<LanderAreaScript>().landingSpot;
                Landing();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "LandingPlatform")
        {
            landingTarget = null;
            canLand = false;
            landButton.SetActive(false);
        }
    }

    public void Stop()
    {
        spaceShipStaminaImage.transform.parent.gameObject.SetActive(true);
        activate = false;
        canMove = true;
        canLaunch = false;
        myRB.useGravity = false;
        myRB.isKinematic = true;
        myRB.velocity = Vector3.zero;
    }

    public void Landing()
    {
        for (int i = 0; i < trails.Length; i++)
        {
            trails[i].SetActive(false);
        }
        activate = false;
        myAnim.enabled = false;
        falling = false;
        speed = normalSpeed;
        parachute.SetActive(false);
        transform.position = landingTarget.position;
        canMove = false;
        launchParticles.Stop();
        transform.eulerAngles = new Vector3(-90f, 0f, 0f);
        gm.ActivateWalk();
        canLand = false;
        landButton.SetActive(false);
        stamina = fullStamina;
        spaceShipStaminaImage.transform.parent.gameObject.SetActive(false);
        myRB.drag = 0f;
        stage1 = false;
        StartCoroutine("CoolDown");
    }
  
    // This event is activated from spaceship animation
    public void Activate()
    {
        myRB.useGravity = true;
        myRB.isKinematic = false;
        myRB.AddForce(Vector3.up * launchPower);
        spaceShipStaminaImage.transform.parent.gameObject.SetActive(false);
    }
    
    public IEnumerator Relaunch()
    {
        yield return new WaitForSeconds(1f);
        stage1 = true;
        activate = true;
        if(boosterSky && levelInfo.levelNumber == 1)
        {
            canLaunch = true;
        }
    }

    public IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(1f);
        canLaunch = true;
    }

    public IEnumerator ChangeLevel(int levelNumber)
    {
        canMove = false;
        fadeScreen.GetComponent<Animator>().SetTrigger("ChangeLevel");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelNumber);
    }
}
