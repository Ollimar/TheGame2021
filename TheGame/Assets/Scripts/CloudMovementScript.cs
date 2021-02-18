using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovementScript : MonoBehaviour
{
    public float speed = 3f;
    public float limit = 638f;
    public float startPos = -446f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if(transform.position.x > limit)
        {
            transform.position = new Vector3(startPos, transform.position.y, transform.position.z);
        }
    }
}
