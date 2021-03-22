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
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if(timer > 0.5f)
        {
            gameObject.SetActive(false);
        }

    }
}
