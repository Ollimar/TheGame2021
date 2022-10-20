using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageInOpeningCutScene : MonoBehaviour
{

    public Transform attachPoint;

    // Start is called before the first frame update
    void Start()
    {
        attachPoint = GameObject.Find("EvilEnemy").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,attachPoint.position.z);
    }
}
