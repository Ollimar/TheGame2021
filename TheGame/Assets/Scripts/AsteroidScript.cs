using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{

    public float speed = 5f;

    private Vector3 startPos;

    public float starAge = 60f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(speed,-speed,0f)* Time.deltaTime);

        starAge -= Time.deltaTime;

        if(starAge <= 0f)
        {
            GetComponent<TrailRenderer>().enabled = false;
            transform.position = startPos;
            StartCoroutine("ResetTrail");
        }
    }

    public IEnumerator ResetTrail()
    {
        starAge = 60f;
        yield return new WaitForSeconds(1f);
        GetComponent<TrailRenderer>().enabled = true;

    }
}
