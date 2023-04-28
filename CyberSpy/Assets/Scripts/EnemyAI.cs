using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent myAgent;
    public LayerMask whatIsGround, whatIsPlayer;
    public Transform player;
    private Animator myAnimator;

    public Transform firePosition;

    //garding behavior
    public Vector3 destinationPoint;
    bool destinationSet;
    public float destinationRange;

    // chasing behavior
    public float chaseRange;
    private bool playerInChaseRange;

    // attack behavior
    public float attackRange, attackTime;
    private bool playerInAttackRange, readyToAttack = true;
    public GameObject attackProjectle;

    public int meleeDamageAmount;

    // melee attack
    public bool meleeAtacker;

    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        myAgent = GetComponent<NavMeshAgent>();
        myAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        playerInChaseRange = Physics.CheckSphere(transform.position, chaseRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInChaseRange && !playerInAttackRange) Guarding();

        if (playerInChaseRange && !playerInAttackRange) ChasingPlayer();

        if (playerInChaseRange && playerInAttackRange) AttackingPlayer();

    }



    private void Guarding()
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
 private void ChasingPlayer()
    {
        myAgent.SetDestination(player.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    private void AttackingPlayer()
    {
        myAgent.SetDestination(transform.position);
        transform.LookAt(player);

        if (readyToAttack && !meleeAtacker)
        {
            myAnimator.SetTrigger("Attack");

            firePosition.LookAt(player);

            Instantiate(attackProjectle, firePosition.position, firePosition.rotation);

            readyToAttack = false;
            StartCoroutine(ResetAttack());
        }
        else if(readyToAttack && meleeAtacker)
        {
            myAnimator.SetTrigger("Attack");
        }
    } 

    public void MeleeDamage()
    {
        if (playerInAttackRange)
        {
            player.GetComponent<PlayerhealthSystem>().TakeDamage(meleeDamageAmount);
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackTime);

        readyToAttack = true;
    }
}
  