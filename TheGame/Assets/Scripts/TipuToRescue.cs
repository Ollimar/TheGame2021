using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipuToRescue : MonoBehaviour
{
    private Rigidbody myRB;
    private Animator myAnim;

    public GameObject[] eyes;
    public ParticleSystem[] tears;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        myRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rescued()
    {
        myAnim.SetTrigger("Rescued");
        myRB.useGravity = true;
        myRB.isKinematic = false;
        tears[0].Stop();
        tears[1].Stop();
        eyes[0].SetActive(false);
        eyes[1].SetActive(true);
    }
}
