using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : Actor
{
    public  EnemySO enemySo;
    
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

    public float speed;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    protected override void Awake()
    {
        if (enemySo != null)
        {
            currentHealth = enemySo.maxHealth;
            speed = enemySo.speed;

            agent = GetComponent<NavMeshAgent>();
            agent.speed = speed;

            player = GameObject.FindGameObjectWithTag("Player").transform;

            if (enemySo.itemsToDrop.Length > 0)
            {
                itemToDrop = enemySo.itemsToDrop[Random.Range(0, enemySo.itemsToDrop.Length)];
            }
        }

        base.Awake();
        
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

        //Debug.Log("Trying walkPoint: " + walkPoint);
        
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
            LaunchProjectileAtPlayer();
            yield return new WaitForSeconds(timeBetweenAttacks);
        }

        alreadyAttacking = false;
    }
    
    private void LaunchProjectileAtPlayer()
    {
        GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
        Rigidbody rb = proj.GetComponent<Rigidbody>();

        if (rb != null && player.transform != null)
        {
            Vector3 velocity = CalculateArcVelocity(transform.position, player.transform.position, 0.5f);
            rb.linearVelocity = velocity;
        }
    }
    private Vector3 CalculateArcVelocity(Vector3 origin, Vector3 target, float height)
    {
        float gravity = Physics.gravity.y;

        // Account for player displacement to enemy
        Vector3 displacementXZ = new Vector3(target.x - origin.x, 0, target.z - origin.z);
        float displacementY = target.y - origin.y;

        // Initial velocity to projectile
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity));

        return velocityXZ + velocityY;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

}