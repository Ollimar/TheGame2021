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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ReturnCollision()
    {
        yield return new WaitForSeconds(0.4f);
        GetComponent<Collider>().enabled = true;
    }
}
