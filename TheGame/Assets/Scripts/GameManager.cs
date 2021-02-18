using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject spaceShip;

    public LevelInfo levelInfo;

    // Variables for allowing player to travel between different heights
    public bool boosterSky              = false;
    public bool boosterSpace            = false;

    public float staminaMultipliers = 1f;

    public float timer;
    public float changeTimer;
    public float moveTimer;
    public float stopTime;
    public float wheelSpeed;
    public int wheels;
    public GameObject[] wheel;
    public bool rotate = false;

    public GameObject sun;
    public Material mySkybox;
    public Material skyBoxDay;
    public Material skyBoxNight;

    public Texture skyDay;
    public Texture skyNight;

    public GameObject[] npc;
    public GameObject[] storyBooks;

    public bool startFromBed = true;

    public GameObject[] stairsToSky;

    // Missions and progression variables
    public bool[] missions;

    public static GameManager gameManager;

    // Variable for checking if we are in spaceship
    public bool flying = false;

    // Variable for fadeScreen that is played when transitioning between levels
    public GameObject fadeScreen;

    // Variable for checking if the player should start from different place than usual. 
    // e.g. Arriving from upstairs spawns the character on stairs rather than front door.
    public bool reverseDirection = false;

    // Camera
    private CameraScript cam;


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Awake()
    {
        if(gameManager == null)
        {
            DontDestroyOnLoad(gameObject);
            gameManager = this;
        }

        else if(gameManager != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        wheel       = GameObject.FindGameObjectsWithTag("Wheel");
        player      = GameObject.FindGameObjectWithTag("Player");
        spaceShip   = GameObject.FindGameObjectWithTag("SpaceShip");
        cam         = Camera.main.GetComponent<CameraScript>();
        sun         = GameObject.Find("Sun");
        fadeScreen  = GameObject.Find("FadeScreen");
        levelInfo   = GameObject.Find("LevelInfo").GetComponent<LevelInfo>();
        spaceShip.GetComponent<SpaceShipScript>().staminaMultiplier = staminaMultipliers;
        stairsToSky = GameObject.FindGameObjectsWithTag("StairsToSky");
        startFromBed = true;

        for(int i=0; i>stairsToSky.Length; i++)
        {
            stairsToSky[i].SetActive(false);
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player      = GameObject.FindGameObjectWithTag("Player");
        spaceShip   = GameObject.FindGameObjectWithTag("SpaceShip");
        cam         = Camera.main.GetComponent<CameraScript>();
        fadeScreen  = GameObject.Find("FadeScreen");
        levelInfo   = GameObject.Find("LevelInfo").GetComponent<LevelInfo>();

        if(levelInfo.levelNumber == 0)
        {
            if(startFromBed)
            {
                player.transform.position = new Vector3(9f, -19.08f, 45.96f);
                player.transform.eulerAngles = new Vector3(0f, -90f, 0f);
            }

            else
            {
                player.transform.position = new Vector3(1.127f, -19.087f, 40.12f);
                player.transform.eulerAngles = new Vector3(0f, -90f, 0f);
            }

            reverseDirection = true;
        }

        if(levelInfo.levelNumber == 2 && reverseDirection)
        {
            player.transform.position       = new Vector3(5.51f, -13f, 48.6f);
            player.transform.eulerAngles    = new Vector3(0f,-270f,0f);
            reverseDirection = false;
            startFromBed = false;
        }

        else if (levelInfo.levelNumber == 2 && !reverseDirection)
        {
            player.transform.position = new Vector3(6.94f, -19.03f, 33.42f);
            startFromBed = false;
        }

        if (levelInfo.levelNumber == 1 && reverseDirection)
        {
            ActivateFly();
            spaceShip.GetComponent<SpaceShipScript>().falling = true;
            spaceShip.GetComponent<SpaceShipScript>().canMove = true;
            spaceShip.transform.position = new Vector3(88f, 8f, 190f);
            cam.FlyCamera();
            reverseDirection = false;
        }

        wheel       = GameObject.FindGameObjectsWithTag("Wheel");
        npc         = GameObject.FindGameObjectsWithTag("NPC");
        spaceShip.GetComponent<SpaceShipScript>().staminaMultiplier = staminaMultipliers;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        // Experimental section for changing skybox with sun rotation
        /*
        sun.transform.Rotate(Vector3.up * 1f * Time.deltaTime);

        if(sun.transform.eulerAngles.y > 50f)
        {
            RenderSettings.skybox = mySkybox;
            print("Night");
            mySkybox.Lerp(mySkybox, skyBoxNight, 0.1f * Time.deltaTime);
        }
        */

        RenderSettings.skybox.SetFloat("_Rotation", Time.time*-1f);

        if(timer > changeTimer)
        {
            moveTimer += Time.deltaTime;
            rotate = true;

            if (moveTimer > stopTime)
            {
                timer = 0f;
                moveTimer = 0f;
                rotate = false;
            }
        }
    }

    void FixedUpdate()
    {

        // This is for rotating the wooden cogs

        /*
            if (rotate)
            {
                wheel[0].transform.Rotate(Vector3.up * wheelSpeed);
                wheel[1].transform.Rotate(Vector3.up * -wheelSpeed);
                wheel[2].transform.Rotate(Vector3.up * -wheelSpeed);
            }
        */

    }

    public void ActivateFly()
    {
        flying = true;
        spaceShip.GetComponent<SpaceShipScript>().activate = true;
        spaceShip.GetComponent<Rigidbody>().isKinematic = false;
        spaceShip.GetComponent<Rigidbody>().useGravity = true;
        cam.FlyCamera();
        player.transform.parent = spaceShip.transform;
        player.transform.position = new Vector3(0f, 0.309f, 0.359f);
        player.GetComponent<PlayerScript>().enabled = false;
    }

    public void ActivateWalk()
    {
        flying = false;
        cam.ReturnCamera();
        spaceShip.GetComponent<Rigidbody>().isKinematic = true;
        spaceShip.GetComponent<Rigidbody>().useGravity = false;
        spaceShip.GetComponent<SpaceShipScript>().activate = false;
        player.transform.parent = null;
        player.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        player.transform.position = new Vector3(spaceShip.transform.position.x,spaceShip.transform.position.y,spaceShip.transform.position.z+5f);
        player.GetComponent<PlayerScript>().enabled = true;
        player.SetActive(true);
    }

    public void SpaceShipStaminaUpdate()
    {
        staminaMultipliers -= 0.1f;
        spaceShip.GetComponent<SpaceShipScript>().staminaMultiplier = staminaMultipliers;
    }

    public IEnumerator LoadLevel(int levelNumber)
    {
        fadeScreen.GetComponent<Animator>().SetTrigger("ChangeLevel");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelNumber);
    }

    public void MissionComplete(int mission)
    {
        missions[mission] = true;
        int nPc = npc[mission].GetComponentInChildren<NPCScript>().npcNumber;
        npc[nPc].GetComponentInChildren<NPCScript>().missionComplete = true;
    }

    public void SkyActivated()
    {
        boosterSky = true;
        spaceShip.GetComponent<SpaceShipScript>().boosterSky = true;
    }
}
