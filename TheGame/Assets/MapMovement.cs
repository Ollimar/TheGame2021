using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;


public class MapMovement : MonoBehaviour
{
    public RotatorScript rotator;
    public GameObject landingPrompt;
    public GameObject landOrLeave;
    public GameObject[] turnipImages;
    public Sprite goldenTurnipCollected;
    public Sprite noTurnipCollected;
    public int levelToLoad;
    public string levelName;

    public bool zoomToLevel = false;
    public bool zoomed = false;

    private Transform cameraParent;
    private Transform cameraTarget;
    private Transform cameraReturnPos;
    private Vector3 cameraReturnposition;
    private Quaternion cameraReturnRotation;

    private GameManager gm;

    // PostProcessEffects
    public PostProcessVolume postProcess;
    public DepthOfField depthOfField;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rotator = GameObject.Find("Rotator").GetComponent<RotatorScript>();
        postProcess = GameObject.Find("PP").GetComponent<PostProcessVolume>();
        postProcess.profile.TryGetSettings(out depthOfField);
        cameraReturnPos = GameObject.Find("CameraPos").transform;
        landingPrompt.SetActive(false);
        landOrLeave.SetActive(false);
        cameraParent = transform.parent;
    }

    private void Update()
    {
        if(landingPrompt.activeSelf && Input.GetButtonDown("Jump"))
        {
            cameraReturnposition = Camera.main.transform.position;
            cameraReturnRotation = Camera.main.transform.rotation;
            Camera.main.transform.parent = null;
            zoomToLevel = true;
        }

        if(zoomToLevel)
        {
            landingPrompt.SetActive(false);
            rotator.canMove = false;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraTarget.transform.position, 2.5f * Time.deltaTime);
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, cameraTarget.transform.rotation, 2.5f * Time.deltaTime);
            depthOfField.focusDistance.value = 7f;
            depthOfField.focalLength.value = 48.51f;
            landOrLeave.SetActive(true);
            landOrLeave.GetComponentInChildren<Text>().text = levelName.ToString();
            
            for(int i=0; i<turnipImages.Length; i++)
            {
                turnipImages[i].GetComponent<Image>().sprite = noTurnipCollected;
            }

            if (levelToLoad == 1)
            {
                for (int i = 0; i < gm.goldenTurnipsGrassLands; i++)
                {
                    turnipImages[i].GetComponent<Image>().sprite = goldenTurnipCollected;
                }
            }

            if (levelToLoad == 2)
            {
                for (int i = 0; i < gm.goldenTurnipsDesert; i++)
                {
                    turnipImages[i].GetComponent<Image>().sprite = goldenTurnipCollected;
                }
            }

            StartCoroutine("Zoomed");
        }

        if (zoomed && Input.GetButtonDown("Fire1"))
        {
            zoomToLevel = false;
        }

        if (zoomed && Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene(levelToLoad);
        }


        if (!zoomToLevel)
        {
            ReturnCamera();
            zoomed = false;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "LandingPlatform")
        {
            landingPrompt.SetActive(true);
            landingPrompt.GetComponentInChildren<Text>().text = other.gameObject.GetComponent<LanderAreaScript>().planetName;
            levelName = other.gameObject.GetComponent<LanderAreaScript>().planetName;
            levelToLoad = other.gameObject.GetComponent<LanderAreaScript>().levelNumber;
            cameraTarget = other.gameObject.GetComponent<LanderAreaScript>().cameraTarget;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "LandingPlatform")
        {
            landingPrompt.SetActive(false);
        }
    }

    public IEnumerator Zoomed()
    {
        yield return new WaitForSeconds(2f);
        zoomed = true;
    }

    public void ReturnCamera()
    {
        Camera.main.transform.parent = cameraParent;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraReturnPos.position, 3f * Time.deltaTime);
        Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, cameraReturnPos.rotation, 3f * Time.deltaTime);
        depthOfField.focusDistance.value = 28.7f;
        depthOfField.focalLength.value = 85f;
        landOrLeave.SetActive(false);
        rotator.canMove = true;
        //zoomToLevel = false;
    }
}
