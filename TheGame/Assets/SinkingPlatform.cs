using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingPlatform : MonoBehaviour
{

    public bool sink = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(sink)
        {
            transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f) * Time.deltaTime;
        }

        if(transform.localScale.x <=0f)
        {
            sink = false;
            transform.localScale = new Vector3(1.49506f, 1.49506f, 1.49506f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            sink = true;
        }
    }
}
