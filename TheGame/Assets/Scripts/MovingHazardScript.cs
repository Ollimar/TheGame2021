using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHazardScript : MonoBehaviour
{

    public float speed = 3f;
    public float timer;
    public float turnTime = 3f;

    public Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > turnTime)
        {
            speed = -speed;
            timer = 0f;
        }

        transform.Translate(moveDirection*speed*Time.deltaTime);
    }
}
