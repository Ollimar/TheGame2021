using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObject : MonoBehaviour
{
    public float speed = 5f;

    public Vector3 originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
