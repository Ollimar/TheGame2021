using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovementScript : MonoBehaviour
{
    public float speed = 3f;
    public float limit = 638f;
    public float startPos = -446f;

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if(transform.position.x > limit)
        {
            transform.position = new Vector3(mainCamera.transform.position.x-100f, transform.position.y, transform.position.z);
        }
    }
}
