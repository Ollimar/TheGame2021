using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;


public class MapMovement : MonoBehaviour
{
    public GameObject landingPrompt;
    public int levelToLoad;

    public bool zoomToLevel = false;
    public bool zoomed = false;

    private Transform cameraParent;
    private Transform cameraTarget;
    private Transform cameraReturnPos;
    private Vector3 cameraReturnposition;
    private Quaternion cameraReturnRotation;

    // PostProcessEffects
    public PostProcessVolume postProcess;
    public DepthOfField depthOfField;

    private void Start()
    {
        postProcess = GameObject.Find("PP").GetComponent<PostProcessVolume>();
        postProcess.profile.TryGetSettings(out depthOfField);
        cameraReturnPos = GameObject.Find("CameraPos").transform;
        landingPrompt.SetActive(false);
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
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraTarget.transform.position, 2f * Time.deltaTime);
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, cameraTarget.transform.rotation, 2f * Time.deltaTime);
            depthOfField.focusDistance.value = 7f;
            depthOfField.focalLength.value = 48.51f;
            StartCoroutine("Zoomed");
        }

        if (zoomed && Input.GetButtonDown("Jump"))
        {
            zoomToLevel = false;
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
        //zoomToLevel = false;
    }
}
