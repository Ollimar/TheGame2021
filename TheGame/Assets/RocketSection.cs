using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSection : MonoBehaviour
{

    public float speed = 5f;
    public float limit = 610.55f;

    public Vector3 trainDirection;
    public int direction;

    public bool playerOn = false;
    // Start is called before the first frame update
    void Start()
    {
        switch (direction)
        {
            case 3:
                trainDirection = new Vector3(0, 0, 1);
                break;
            case 2:
                trainDirection = new Vector3(1, 0, 0);
                break;
            case 1:
                trainDirection = new Vector3(0, 0, -1);
                break;
            default:
                trainDirection = new Vector3(-1, 0, 0);
                break;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerOn && transform.position.z < limit)
        {
            transform.Translate(trainDirection * speed * Time.deltaTime);
        }
    }


    public IEnumerator StartEngine()
    {
        yield return new WaitForSeconds(2f);
        playerOn = true;
    }
}
