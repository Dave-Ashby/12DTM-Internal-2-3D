using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Animator anim;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    public float walkPointRange;
    public bool walkPointSet;

    //Attacking
    public bool alreadyAttacked;
    public float timeBetweenAttacks;
    public float health = 10f;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    void Patrolling()
    {
        anim.SetBool("Patrolling", true);
        anim.SetBool("Attacking", false);
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }

    void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, 0, 0);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) walkPointSet = true;
    }

    void ChasePlayer()
    {
        anim.SetBool("Patrolling", true);
        anim.SetBool("Attacking", false);

        agent.SetDestination(player.position);
        Vector3 targetPostition = new Vector3(player.position.x, this.transform.position.y, player.position.z);
        this.transform.LookAt(targetPostition);

    }

    void AttackPlayer()
    {
        anim.SetBool("Patrolling", false);
        anim.SetBool("Attacking", true);

        agent.SetDestination(transform.position);
        Vector3 targetPostition = new Vector3(player.position.x, this.transform.position.y, player.position.z);
        this.transform.LookAt(targetPostition);

        if (!alreadyAttacked)
        {
            //Insert Attack Code


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false; 
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 2f);
    }

    void DestroyEnemy()
    {
        //Death Animation Code
    }
}

