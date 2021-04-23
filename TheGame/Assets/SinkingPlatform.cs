using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingPlatform : MonoBehaviour
{

    public bool sink = false;
    public bool collisionSink = false;
    private Vector3 originalScale;
    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(sink)
        {
            transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f) * Time.deltaTime;

        }

        if(transform.localScale.x <=0f)
        {
            GetComponent<Collider>().enabled = false;
            transform.localScale = new Vector3(0f, 0f, 0f);
            StartCoroutine("ReGenerate");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            sink = true;
        }
    }

    public IEnumerator CollisionSink()
    {
        yield return new WaitForSeconds(1f);
        sink = true;
    }

    public IEnumerator ReGenerate()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<Collider>().enabled = true;
        sink = false;
        collisionSink = false;
        transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
    }
}
