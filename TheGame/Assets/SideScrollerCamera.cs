using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerCamera : MonoBehaviour
{
    public Transform target;

    public float yOffSet = 2f;
    public float xOffSet = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x + xOffSet, target.position.y + yOffSet, transform.position.z),1f);
    }
}
