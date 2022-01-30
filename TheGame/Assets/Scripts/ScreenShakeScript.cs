using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeScript : MonoBehaviour
{
    public IEnumerator ScreenShake(float duration, float magnitude)
    {  
        Vector3 originalPos = transform.position;

        float elapsed = 0f;

        while(elapsed < duration)
        {
            float x = Random.Range(transform.position.x- 1f, transform.position.x+ 1f) * magnitude;
            float y = Random.Range(transform.position.y - 0.3f, transform.position.y +0.3f) * magnitude;

            transform.localPosition = new Vector3(x,y,originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
            print("Screen is shaking!");
        }

        transform.position = originalPos;
    }
}
