using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{

    private GameManager gm;

    public GameObject object1;
    public GameObject object2;
    public GameObject object3;

    public int object1Price = 30;
    public int object2Price = 30;
    public int object3Price = 30;

    public GameObject object1Info;

    public GameObject[] level1Objects;
    public GameObject[] level2Objects;
    public GameObject[] level3Objects;
    public GameObject[] level4Objects;
    public GameObject[] level5Objects;
    public GameObject[] level6Objects;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        object1Info.SetActive(false);

        if(gm.level1Beaten && !gm.level2Beaten)
        {
            GameObject obj1 = level1Objects[Random.Range(0,level1Objects.Length)];
            Instantiate(obj1, object1.transform.GetChild(0).transform.position, object1.transform.GetChild(0).transform.rotation);

            GameObject obj2 = level1Objects[Random.Range(0, level1Objects.Length)];
            Instantiate(obj2, object2.transform.GetChild(0).transform.position, object2.transform.GetChild(0).transform.rotation);

            GameObject obj3 = level1Objects[Random.Range(0, level1Objects.Length)];
            Instantiate(obj3, object3.transform.GetChild(0).transform.position, object3.transform.GetChild(0).transform.rotation);
        }

        else if (gm.level1Beaten && gm.level2Beaten)
        {
            int num1 = Random.Range(0, 2);
            int num2 = Random.Range(0, 2);
            int num3 = Random.Range(0, 2);


            // Shuffle Object1
            if(num1 == 0)
            {
                GameObject obj1 = level1Objects[Random.Range(0, level1Objects.Length)];
                Instantiate(obj1, object1.transform.GetChild(0).transform.position, object1.transform.GetChild(0).transform.rotation);
            }

            else if(num1 == 1)
            {
                GameObject obj1 = level2Objects[Random.Range(0, level2Objects.Length)];
                Instantiate(obj1, object1.transform.GetChild(0).transform.position, object1.transform.GetChild(0).transform.rotation);
            }

            // Shuffle Object2
            if(num2 == 0)
            {
                GameObject obj2 = level1Objects[Random.Range(0, level1Objects.Length)];
                Instantiate(obj2, object2.transform.GetChild(0).transform.position, object2.transform.GetChild(0).transform.rotation);
            }

            else if(num2 == 1)
            {
                GameObject obj2 = level2Objects[Random.Range(0, level2Objects.Length)];
                Instantiate(obj2, object2.transform.GetChild(0).transform.position, object2.transform.GetChild(0).transform.rotation);
            }

            //Shuffle Object3
            if (num3 == 0)
            {
                GameObject obj3 = level1Objects[Random.Range(0, level1Objects.Length)];
                Instantiate(obj3, object3.transform.GetChild(0).transform.position, object3.transform.GetChild(0).transform.rotation);
            }

            else if (num3 == 1)
            {
                GameObject obj3 = level2Objects[Random.Range(0, level2Objects.Length)];
                Instantiate(obj3, object3.transform.GetChild(0).transform.position, object3.transform.GetChild(0).transform.rotation);
            }
        }
    }



    public void Object1()
    {
        if(gm.coins > object1Price)
        {
            gm.coins = gm.coins - object1Price;
        }
        else
        {
            return;
        }
    }
}
