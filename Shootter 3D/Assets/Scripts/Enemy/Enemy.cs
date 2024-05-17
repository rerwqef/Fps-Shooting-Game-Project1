using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, WhatIsPlayer;

    public float walkPointRange = 10f;
    public float timeBetweenAttacks = 2f;
    bool alreadyAttacked;
    public GameObject projectile;
    public float sightRange = 25f, attackRange = 2f;
    public bool playerInSightRange, playerInAttackRange;
    Animator anim;

    public float rotationSpeed = 5f;

    private Vector3 walkPoint;
    private bool reachedWalkPoint;
    [SerializeField] float AttackSPeedSPEED;
    [SerializeField] float NormalSPEED;
    [SerializeField] float SPEED;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
    }

    private void Patroling()
    {
        if (!agent.hasPath || agent.remainingDistance < 1f)
        {
            if (!reachedWalkPoint)
            {
                reachedWalkPoint = true;
                Invoke(nameof(ResetReachedWalkPoint), 2f);
            }
            else
            {
                SearchWalkPoint();
            }
        }
    }

    private void ResetReachedWalkPoint()
    {
        reachedWalkPoint = false;
    }

    private void SearchWalkPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkPointRange;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkPointRange, 1);
        agent.SetDestination(hit.position);

        if (Physics.Raycast(hit.position, -transform.up, 2f, whatIsGround))
        {
            anim.SetBool("Walk", true);
            agent.speed = NormalSPEED;
            anim.SetBool("Idle", false);
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);

    }

    private void Attacking()
    {
        agent.SetDestination(transform.position);
      //  transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // Instantiate and launch the projectile (you can uncomment this part)
            // Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            // rb.AddForce(transform.forward * 32, ForceMode.Impulse);
            // rb.AddForce(transform.up * 8, ForceMode.Impulse);
            agent.speed = AttackSPeedSPEED;
            anim.SetBool("Walk", false);
            anim.SetBool("Idle", true);
            anim.SetBool("Attack", true);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttacking), timeBetweenAttacks);
        }
    }

    private void ResetAttacking()
    {
        alreadyAttacked = false;

        anim.SetBool("Attack", false);
        agent.speed = NormalSPEED;
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
            Patroling();
        else if (playerInSightRange && !playerInAttackRange)
            ChasePlayer();
        else if (playerInAttackRange && playerInSightRange)
            Attacking();
    }

 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}