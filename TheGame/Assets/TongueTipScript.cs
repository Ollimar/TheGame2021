using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueTipScript : MonoBehaviour
{

    private TongueScript tongueScript;
    // Start is called before the first frame update
    void Start()
    {
        tongueScript = GameObject.Find("TongueBase").GetComponent<TongueScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            tongueScript.attachedEnemy = other.transform.gameObject;
            other.GetComponent<Collider>().isTrigger = true;
        }

        if (other.gameObject.tag == "Player")
        {
    

            if(tongueScript.attached)
            {
                tongueScript.attached = false;
            }
        }

        if (other.gameObject.tag == "AttachPoint")
        {
            tongueScript.playerScript.canMove = false;
            tongueScript.attached = true;
            tongueScript.hitPoint = other.transform.position;
            transform.position = tongueScript.hitPoint;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "AttachPoint")
        {
            tongueScript.attached = false;
        }
    }
}
