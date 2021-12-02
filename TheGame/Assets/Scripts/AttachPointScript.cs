using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPointScript : MonoBehaviour
{
    public Vector3 startPoint;
    public Transform floatPoint;
    public Vector3 endPoint;

    public float delay = 0.5f;
    public float dropDelay = 3f;
    public float floatSpeed = 1f;
    public float changeTime = 0.5f;

    public bool attached = false;
    public bool returning = false;

    // Start is called before the first frame update
    void Start()
    {
        floatPoint = transform.GetChild(0);
        startPoint = transform.position;
        floatPoint.position = transform.position;
        endPoint = new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z);

        Transform ch = transform.GetChild(0);
        transform.DetachChildren();

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

        if(transform.position == floatPoint.position)
        {
            returning = false;
        }

        if(attached)
        {
            transform.position = Vector3.Lerp(transform.position, endPoint, dropDelay * Time.deltaTime);
        }

        else if(returning)
        {
            transform.position = Vector3.Lerp(transform.position, floatPoint.position, dropDelay * Time.deltaTime);
        }

        else
        {
            transform.position = floatPoint.position;
        }

        floatPoint.Translate(Vector3.up * floatSpeed * Time.deltaTime);
    }

    private void OnTriggerExit(Collider other)
    {
        returning = true;
        attached = false;
    }

}
