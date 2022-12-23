using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipuToRescue : MonoBehaviour
{
    public bool inCutScene = true;

    private Rigidbody myRB;
    private Animator myAnim;

    public GameObject[] eyes;
    public ParticleSystem[] tears;
    public GameObject tear;
    public GameObject sweat;
    public GameObject puff;

    public bool isRunning = false;

    public float speed = 1f;
    public float newRotation;

    public bool small = true;
    public float scaleSpeed = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0f;
        myAnim = GetComponent<Animator>();
        myRB = GetComponent<Rigidbody>();
        GameObject newObject = new GameObject();
        Instantiate(newObject, transform.position, transform.rotation);
        newObject.transform.parent = transform;
        newObject.transform.position = transform.position;
        SphereCollider newCol = newObject.AddComponent<SphereCollider>();
        newCol.isTrigger = true;
        newCol.radius = 5f;
        newCol.tag = "LevelGoal";

        // Set the caharacter initial size to small so it can walk through the door
        if (small && inCutScene)
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }

        if(sweat != null)
        {
            sweat.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if(!small && inCutScene)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.75f, 0.75f, 0.75f), scaleSpeed * Time.deltaTime);
        }
    }

    public void Running()
    {
        myAnim.SetBool("Running", true);
        if(tears[0] != null)
        {
            tears[0].Stop();
            tears[1].Stop();
            eyes[0].SetActive(false);
            eyes[1].SetActive(true);
        }
        speed = 5f;
    }

    public void Stop()
    {
        speed = 0f;
        myAnim.SetBool("Running", false);
    }

    public void Wave()
    {
        myAnim.SetBool("Wave", true);
    }

    public void Rescued()
    {
        myAnim.SetTrigger("Rescued");
        myRB.useGravity = true;
        myRB.isKinematic = false;
        tears[0].Stop();
        tears[1].Stop();
        eyes[0].SetActive(false);
        eyes[1].SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "RotatorTrigger")
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,newRotation,transform.eulerAngles.z);
        }
    }

    public void SadFace()
    {
        eyes[0].SetActive(true);
        eyes[1].SetActive(false);
    }

    public void HappyFace()
    {
        eyes[0].SetActive(false);
        eyes[1].SetActive(true);
    }
}
