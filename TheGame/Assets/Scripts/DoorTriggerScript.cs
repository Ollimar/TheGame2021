using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerScript : MonoBehaviour
{
    public Animator doorAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            doorAnimator.SetBool("DoorOpen", true);
            doorAnimator.SetBool("DoorClose", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            doorAnimator.SetBool("DoorOpen", false);
            doorAnimator.SetBool("DoorClose", true);
        }
    }

    public void MoveDoor()
    {
        doorAnimator.SetTrigger("Door");
    }
}
