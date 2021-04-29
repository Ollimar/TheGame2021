using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    private PlayerScript player;

    public GameObject tongueStart;
    public GameObject tongueEnd;

    public GameObject tonguePosition;
    public GameObject attachedObject;

    public float tongueSpeed = 5f;
    public float lickDuration = 0.5f;

    public bool attached = false;


    // Start is called before the first frame update
    void Start()
    {
        tongueStart = GameObject.Find("TongueStart");
        tongueEnd = GameObject.Find("TongueMaximum");
        tonguePosition = GameObject.Find("TonguePosition");
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.Lerp(transform.position,tonguePosition.transform.position, tongueSpeed*Time.deltaTime);

        if(Input.GetButtonDown("Fire1"))
        {
            player.canMove = false;
            tonguePosition.transform.position = tongueEnd.transform.position;
            StartCoroutine("Return");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            tonguePosition.transform.position = tongueEnd.transform.position;
            attachedObject = other.gameObject;
            Destroy(other.gameObject);
        }
    }

    public IEnumerator Return()
    {
        yield return new WaitForSeconds(lickDuration);
        tonguePosition.transform.position = tongueStart.transform.position;
        player.canMove = true;

        if(attachedObject != null)
        {
            Destroy(attachedObject);
        }
    }
}
