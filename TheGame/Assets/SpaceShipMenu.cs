using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMenu : MonoBehaviour
{

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        transform.position = Vector3.Lerp(transform.position, target.position, 3f * Time.deltaTime);
    }
}
