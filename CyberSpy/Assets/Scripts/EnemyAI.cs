using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent myAgent;
    public LayerMask whatIsGround;

    public Vector3 destinationPoint;
    bool destinationSet;
    public float destinationRange;

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        Garding();
    }

    private void Garding()
    {
        if (!destinationSet)
            SearchForDestination();
        else
            myAgent.SetDestination(destinationPoint);

        Vector3 distanceToDestination = transform.position - destinationPoint;

        if (distanceToDestination.magnitude < 1f)
            destinationSet = false;
    }

    private void SearchForDestination()
    {
        float randomPositionZ = Random.Range(-destinationRange, destinationRange);
        float randomPositionX = Random.Range(-destinationRange, destinationRange);

        destinationPoint = new Vector3(
            transform.position.x + randomPositionX,
            transform.position.y,
            transform.position.z + randomPositionZ
            );

        if (Physics.Raycast(destinationPoint, -transform.up, 2f, whatIsGround))
            destinationSet = true;
    }
}
