using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullObject : MonoBehaviour
{
    public Transform endPosition;

    public bool pull = false;

    public float transitionTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pull)
        {
            
            transform.position = Vector3.Lerp(transform.position, endPosition.position, 10f * Time.deltaTime);
            StartCoroutine("Pull");
        }
    }

    public IEnumerator StartPull()
    {
        yield return new WaitForSeconds(transitionTime);
        pull = true;
    }

    public IEnumerator Pull()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.tag = "Untagged";
        GameObject.Find("TongueBase").GetComponent<Tongue>().attachedObject = null;
        pull = false;
        Destroy(this);
    }
}
