using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCameraController : MonoBehaviour
{
    public float speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("CameraHor");
        float ver = Input.GetAxis("CameraVer");
        float up = Input.GetAxis("CameraUp");
        float down = Input.GetAxis("CameraDown");

        float verMov = 0f;

        if(up>=0.1f)
        {
            verMov = up;
        }
        else if(down >= 0.1f)
        {
            up = 0f;
            verMov = down;
        }
        transform.Translate(hor * speed * Time.deltaTime, verMov * speed * Time.deltaTime, ver * Time.deltaTime * speed);
    }
}
