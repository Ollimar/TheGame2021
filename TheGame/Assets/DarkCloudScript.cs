using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkCloudScript : MonoBehaviour
{

    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f, -speed * Time.deltaTime, 0f);
    }
}
