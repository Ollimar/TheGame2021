using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSeedScript : MonoBehaviour
{
    public GameObject[] plants;
    public GameObject activePlant;

    public float growthSpeed = 3f;

    private Collider myCollider;

    public bool growing = false;

    // Start is called before the first frame update
    void Start()
    {
        Grow();
    }

    // Update is called once per frame
    void Update()
    {
        if(activePlant != null && activePlant.transform.localScale.x <= 1.5f)
        {
            activePlant.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f)* growthSpeed * Time.deltaTime;
        }
        else if(myCollider != null)
        {
            myCollider.enabled = true;
        }

        if(transform.childCount == 0 && !growing)
        {
            StartCoroutine("Grow");
        }
    }

    public IEnumerator Grow()
    {
        growing = true;
        yield return new WaitForSeconds(1f);
        GameObject newPlant = Instantiate(plants[0], transform.position, transform.rotation);
        newPlant.transform.parent = transform;
        newPlant.transform.localScale = new Vector3(0f, 0f, 0f);
        activePlant = newPlant;
        myCollider = newPlant.GetComponent<Collider>();
        myCollider.enabled = false;
        growing = false;
    }
}
