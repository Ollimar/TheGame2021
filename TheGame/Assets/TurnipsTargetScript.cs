using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnipsTargetScript : MonoBehaviour
{
    public int hits;

    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Turnip")
        {
            hits++;
            other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<Collider>().enabled = false;

            if (hits >= 3)
            {
                gm.MissionComplete(0);
            }
        }
    }
}
