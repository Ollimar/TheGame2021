using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepScript : MonoBehaviour
{

    public Color color1;
    public Color color2;

    public bool fading = false;

    public float stepAge = 5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Fade");
    }


    public IEnumerator Fade()
    {
        yield return new WaitForSeconds(stepAge);
        DestroyStep();
    }

    public void DestroyStep()
    {
        Destroy(gameObject);
    }
}
