using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeShipScript : MonoBehaviour
{

    public Vector3 rotationDirection;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationDirection, speed * Time.deltaTime);
    }
}
