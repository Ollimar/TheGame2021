using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent myNav;

    // Start is called before the first frame update
    void Start()
    {
        myNav = GetComponent<NavMeshAgent>(); 
    }

    // Update is called once per frame
    void Update()
    {
        myNav.destination = target.position;
    }
}
