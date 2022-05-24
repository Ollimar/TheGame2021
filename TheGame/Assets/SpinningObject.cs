using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObject : MonoBehaviour
{
    public float speed = 5f;

    private float originalSpeed;

    // speed that sets when character tongue hits it
    public float hitSpeed;

    public Vector3 originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.eulerAngles;
        originalSpeed = speed;
        hitSpeed = speed * 4f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }


    // Increases the coin spin speed when player tongue hits it
    public IEnumerator IncreasedSpeed()
    {
        print("Hit the coin");
        speed = hitSpeed;
        yield return new WaitForSeconds(0.5f);
        speed = Mathf.Lerp(speed, originalSpeed, 1f*Time.deltaTime);
    }
}
