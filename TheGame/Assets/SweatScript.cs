using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweatScript : MonoBehaviour
{
    private Vector3 startPos;

    private float speed = 3f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        InvokeRepeating("Up", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    public void Up()
    {
        transform.position = startPos;
    }
}
