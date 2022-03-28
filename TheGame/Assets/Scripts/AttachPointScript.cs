using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPointScript : MonoBehaviour
{
    public Vector3 startPoint;
    public Transform floatPoint;
    public Vector3 endPoint;
    public Transform[] wings;
    public Transform[] eyes;
    public Transform[] eyesShut;

    public float delay = 0.5f;
    public float dropDelay = 3f;
    public float floatSpeed = 1f;
    public float changeTime = 0.5f;

    public bool attached = false;
    public bool returning = false;

    public Animator wingAnim;
    public Animator wingAnim2;

    // Start is called before the first frame update
    void Start()
    {
        floatPoint = transform.GetChild(0);
        startPoint = transform.position;
        floatPoint.position = transform.position;
        endPoint = new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z);
        eyesShut[0].gameObject.SetActive(false);
        eyesShut[1].gameObject.SetActive(false);

        Transform ch = transform.GetChild(0);
        ch.parent = null;

        if(transform.parent  != null)
        {
            ch.transform.parent = transform.parent;
        }
    }

    // Update is called once per frame
    void Update()
    {
        changeTime -= Time.deltaTime;

        if(changeTime <= 0f)
        {
            floatSpeed = -floatSpeed;
            changeTime = 0.5f;
        }

        if(transform.position.y >= floatPoint.position.y-0.1f)
        {
            returning = false;
            wingAnim.SetTrigger("Rise");
            wingAnim2.SetTrigger("Rise");
        }

        if(attached)
        {
            transform.position = Vector3.Lerp(transform.position, endPoint, dropDelay * Time.deltaTime);
            wings[0].position = Vector3.Lerp(wings[0].position, endPoint, dropDelay * Time.deltaTime);
            wings[1].position = Vector3.Lerp(wings[1].position, endPoint, dropDelay * Time.deltaTime);
            eyes[0].gameObject.SetActive(false);
            eyes[1].gameObject.SetActive(false);
            eyesShut[0].gameObject.SetActive(true);
            eyesShut[1].gameObject.SetActive(true);
        }

        else if(returning)
        {
            transform.position = Vector3.Lerp(transform.position, floatPoint.position, dropDelay * Time.deltaTime);
            wings[0].position = Vector3.Lerp(wings[0].position, floatPoint.position, dropDelay * Time.deltaTime);
            wings[1].position = Vector3.Lerp(wings[1].position, floatPoint.position, dropDelay * Time.deltaTime);
            wingAnim.SetTrigger("Rise");
            wingAnim2.SetTrigger("Rise");
        }

        else
        {
            transform.position = floatPoint.position;
            wings[0].position = floatPoint.position;
            wings[1].position = floatPoint.position;
            eyes[0].gameObject.SetActive(true);
            eyes[1].gameObject.SetActive(true);
            eyesShut[0].gameObject.SetActive(false);
            eyesShut[1].gameObject.SetActive(false);

        }

        floatPoint.Translate(Vector3.up * floatSpeed * Time.deltaTime);
    }

    private void OnTriggerExit(Collider other)
    {
        returning = true;
        attached = false;
    }

}
