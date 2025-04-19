using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
        
    private Transform player;
    
    public GameObject projectile;
    
    public LayerMask whatIsGround, whatIsPlayer;
    
    public Vector3 walkPoint;
    private bool walkPointSet;  
    public float walkPointRange;
    
    public float timeBetweenAttacks;
    private float attackCooldown;
    private bool alreadyAttacking = false;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }
        else if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        else if (playerInSightRange && playerInAttackRange)
        {
            agent.SetDestination(transform.position);
            transform.LookAt(player);

            if (!alreadyAttacking)
            {
                StartCoroutine(AttackRoutine());
            }
        }
        // print("Player in Sight: " + playerInSightRange + "Player in Range " + playerInAttackRange);
        // OnDrawGizmosSelected();
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
            
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < agent.stoppingDistance)
        {
            
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        Debug.Log("Trying walkPoint: " + walkPoint);
        
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private IEnumerator AttackRoutine()
    {
        alreadyAttacking = true;

        while (playerInSightRange && playerInAttackRange)
        {
            // Attack logic here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 10, ForceMode.Impulse);
            rb.AddForce(transform.up * 8, ForceMode.Impulse);

            yield return new WaitForSeconds(timeBetweenAttacks);
        }

        alreadyAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

}
