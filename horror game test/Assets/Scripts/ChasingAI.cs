using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public Transform player;

    [Header("Movement")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 8;

    [Header("Detection Ranges")]
    public float detectionRange = 10f;
    public float losePlayerRange = 20f;
    public float waypointTolerance = 1f;
    public float killRange = 3f;

    private NavMeshAgent agent;

    public Animator animator;

    private int currentPatrolIndex = 0;

    private State lastLoggedState;

    private float delTime;

    private enum State
    {
        Patrol = 0,
        Chase = 1,
        Kill = 2
    }

    private State currentState = State.Patrol;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        
        if (patrolPoints.Length > 0)
        {
            agent.speed = patrolSpeed;
            agent.stoppingDistance = 0f;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void Update()
    {

        delTime = Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        

        switch (currentState)
        {
            case State.Patrol:
            {
                PatrolState(distanceToPlayer);
                break; 
            }
                
            case State.Chase:
            {
                ChaseState(distanceToPlayer);
                break;
            }
                
            case State.Kill:
            {
                KillState();
                break; 
            }

        }

        if (currentState != lastLoggedState)
        {
            Debug.Log("Enemy state changed to: " + currentState);
            lastLoggedState = currentState;
        }

        animator.SetInteger("State", (int)currentState);

    }

    void PatrolState(float distanceToPlayer)
    {
        if (distanceToPlayer <= detectionRange)
        {
            currentState = State.Chase;
            agent.stoppingDistance = 1.2f;
            return;
        }

        agent.speed = patrolSpeed;

        if (patrolPoints.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance <= waypointTolerance)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void ChaseState(float distanceToPlayer)
    {
        agent.SetDestination(player.position);

        agent.speed = chaseSpeed;

        if (distanceToPlayer >= losePlayerRange)
        {
            currentState = State.Patrol;
            agent.stoppingDistance = 0f;

            if (patrolPoints.Length > 0)
            {
                agent.SetDestination(patrolPoints[currentPatrolIndex].position);
            }
        }

        if (distanceToPlayer <= killRange)
        {
            currentState = State.Kill;
            agent.speed = 0;
        }

    }

    void KillState()
    {
        
    }

    void UpdateAnimator(int stateNum)
    {
        animator.SetInteger("State", stateNum);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, losePlayerRange);
    }
}