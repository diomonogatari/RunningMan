using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ConeMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    private Vector3 destination;
    // Use this for initialization
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        NewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if we almost arrived to our destination, if true, get me a new place to go
        if (Vector3.Distance(transform.position, destination) < 0.3f) NewDestination();
    }

    void NewDestination()
    {
        //Positive X moves right, then switches to negative X
        float positionX = 8;
        if (transform.position.x > 0) positionX = -8;

        destination = new Vector3(positionX, transform.position.y, transform.position.z);

        navMeshAgent.destination = destination;
    }
}
