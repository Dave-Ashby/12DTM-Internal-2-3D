using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyController : MonoBehaviour
{
    public Animator anim;
    public string deathAnim = "Death";

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public TextMeshProUGUI winText;

    //Patroling
    public Vector3 walkPoint;
    public float walkPointRange;
    public bool walkPointSet;

    //Attacking
    public bool alreadyAttacked;
    public float timeBetweenAttacks;
    public float health = 5f;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        winText.enabled = false;
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
        if (health >= 0)
        {
            float randomX = Random.Range(-walkPointRange, walkPointRange);
            walkPoint = new Vector3(transform.position.x + randomX, this.transform.position.y, 0);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) walkPointSet = true;
        }
    }

    void ChasePlayer()
    {
        if (health >= 0)
        {
            anim.SetBool("Patrolling", true);
            anim.SetBool("Attacking", false);

            agent.SetDestination(player.position);
            Vector3 targetPostition = new Vector3(player.position.x, this.transform.position.y, player.position.z);
            this.transform.LookAt(targetPostition);
        }
    }

    void AttackPlayer()
    {
        if (health >= 0)
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
    }

    void ResetAttack()
    {
        alreadyAttacked = false; 
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health == 0) Invoke(nameof(DestroyEnemy), 0.0f);
    }

    void DestroyEnemy()
    {
        //Death Animation Code
        anim.Play(deathAnim, 0, 0.0f);
        winText.enabled = true;
        Destroy(gameObject, 3);
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.CompareTag("Spike"))
        {
            TakeDamage(1);
            Debug.Log("hit");
        }
    }
}

