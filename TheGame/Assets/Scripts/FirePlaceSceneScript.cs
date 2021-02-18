using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlaceSceneScript : MonoBehaviour
{
    public Transform firePlace;

    public float rotateSpeed = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        firePlace.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
