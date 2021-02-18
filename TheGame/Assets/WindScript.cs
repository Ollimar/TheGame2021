using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScript : MonoBehaviour
{
    private Animator myAnim;
    private Transform mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.transform;
        myAnim = GetComponent<Animator>();
        float r = Random.Range(0, 2);
        if (r == 0)
        {
            myAnim.SetTrigger("ChangeWind");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.position = new Vector3(mainCamera.position.x, mainCamera.position.y, mainCamera.position.z-5f);
    }

    public void ChangePosition()
    {
        transform.parent.position = new Vector3(transform.parent.position.x + (Random.Range(-5f, 5f)), transform.parent.position.y + (Random.Range(-5f, 5f)), transform.parent.position.z + (Random.Range(-5f, 5f)));
        float r = Random.Range(0, 2);
        if(r==0)
        {
            myAnim.SetTrigger("ChangeWind");
        }
    }
}
