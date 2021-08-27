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
        //float up = Input.GetAxis("CamerauP");
        transform.Translate(hor * speed * Time.deltaTime, 0f, ver * Time.deltaTime * speed);
    }
}
