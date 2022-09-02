using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusEnemy : MonoBehaviour
{

    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Change", 1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * -speed * Time.deltaTime);
    }

    public void Change()
    {
        speed = -speed;
    }
}
