using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public Transform spaceShipTarget;

    public PlayerScript player;

    // Camera Targets

    public Transform flyTarget;
    public Transform landTarget;

    public float cameraHeight = 3f;
    public float cameraDistance = 3f;

    public float cameraRotation = 15f;

    public float cameraSmoothing = 0.1f;

    public bool canMoveCamera = true;
    public bool dialogueOn = false;

    // Cameras
    public Camera doorCamera;

    // PostProcessEffects
    public PostProcessVolume     postProcess;
    public DepthOfField          depthOfField;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        target = GameObject.Find("CameraTarget").transform;
        landTarget = GameObject.Find("CameraTarget").transform;
        postProcess = GameObject.Find("PP").GetComponent<PostProcessVolume>();
        postProcess.profile.TryGetSettings(out depthOfField);
        spaceShipTarget = GameObject.Find("SpaceShip").transform;
        player.gameStarted = false;
        ReturnCamera();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + cameraHeight, target.position.z - cameraDistance);
        transform.position = Vector3.Lerp(transform.position, newPos, cameraSmoothing*Time.deltaTime);
        Vector3 newRot = new Vector3(cameraRotation, transform.rotation.y, transform.rotation.z);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, newRot, cameraSmoothing * Time.deltaTime);

        if(canMoveCamera)
        {
            if (Input.GetAxis("Mouse Y") > 0.1f && cameraDistance > 10f || Input.GetAxis("CameraVertical") > 0.1f && cameraDistance > 10f)
            {
                cameraDistance = 10;
                cameraHeight = 5f;
                cameraRotation = 15f;
            }
            else if (Input.GetAxis("Mouse Y") < -0.1f && cameraDistance < 12f || Input.GetAxis("CameraVertical") < -0.1f && cameraDistance < 12f)
            {
                cameraDistance = 12f;
                cameraHeight = 14f;
                cameraRotation = 50f;
            }
        }
    }

    public void OverHeadCamera()
    {
        canMoveCamera = false;
        cameraDistance = 5f;
        cameraHeight = 9f;
        cameraRotation = 60f;
    }

    public void FlyCamera()
    {
        canMoveCamera = false;
        cameraDistance = 25f;
        cameraHeight = 20f;
        cameraRotation = 45f;
        target = flyTarget;
    }

    public void DialogueCamera()
    {
        cameraDistance = 7f;
        cameraHeight = 3f;
        cameraRotation = 15f;
        canMoveCamera = false;
        depthOfField.focusDistance.value = 7f;
    }

    public void ReturnCamera()
    {
        cameraDistance = 12f;
        cameraHeight = 14f;
        cameraRotation = 50f;
        canMoveCamera = true;
        target = landTarget;
        depthOfField.focusDistance.value = 13f;
    }

    //Coroutine called from the KeyScript which returns the camera from showing the unlocked door to the player
    public IEnumerator DoorToPlayer()
    {
        yield return new WaitForSeconds(3f);
        Camera.main.depth = 0;
        doorCamera.depth = -1;
        GameObject.Find("Player").GetComponent<PlayerScript>().canMove = true;
    }
}
