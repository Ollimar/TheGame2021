using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrain : MonoBehaviour
{
    public int movementDirection;
    public Transform startPoint;
    public Vector3 direction;

    public float speed = 2f;

    private float pos;

    // Start is called before the first frame update
    void Start()
    {
        switch (movementDirection)
        {
            case 3:
                direction = new Vector3(0, 0, 1);
                break;
            case 2:
                direction = new Vector3(1, 0, 0);
                break;
            case 1:
                direction = new Vector3(0, 0, -1);
                break;
            default:
                direction = new Vector3(-1, 0, 0);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "TrainPumber")
        {
            transform.position = startPoint.position;
        }
    }
}
