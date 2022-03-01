using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSection : MonoBehaviour
{

    public float speed = 5f;

    public bool playerOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerOn && transform.position.z < 610.55)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }


    public IEnumerator StartEngine()
    {
        yield return new WaitForSeconds(2f);
        playerOn = true;
    }
}
