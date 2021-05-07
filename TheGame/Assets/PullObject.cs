using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullObject : MonoBehaviour
{
    public Transform endPosition;

    public bool canPull = true;
    public bool pull = false;

    public float transitionTime = 1f;

    public ParticleSystem[] effects;

    // Start is called before the first frame update
    void Start()
    {
        effects[0].Stop();
        effects[1].Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(pull && canPull)
        {
            effects[0].Play();
            effects[1].Play();
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
        gameObject.tag = "Untagged";
        GameObject.Find("TongueBase").GetComponent<Tongue>().attachedObject = null;
        yield return new WaitForSeconds(1f);
        pull = false;
        canPull = false;
        effects[0].Stop();
        effects[1].Stop();
        Destroy(this);
    }
}
