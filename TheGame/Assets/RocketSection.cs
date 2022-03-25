using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSection : MonoBehaviour
{

    public float speed = 5f;
    public float limit = 610.55f;

    public bool playerOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerOn && transform.position.z < limit)
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
