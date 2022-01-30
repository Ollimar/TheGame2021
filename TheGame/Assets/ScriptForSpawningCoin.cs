using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptForSpawningCoin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider>().enabled = false;
        StartCoroutine("ReturnCollision");
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < 0.8f)
        {
            transform.localScale += new Vector3(3f, 3f, 3f) * Time.deltaTime;
            GetComponent<SphereCollider>().radius = 1f;
        }
    }

    public IEnumerator ReturnCollision()
    {
        yield return new WaitForSeconds(0.4f);
        GetComponent<Collider>().enabled = true;
    }
}
