using UnityEngine;
using UnityEngine.AI;

public enum EnemyState { Idle, Patrol, Chase, Attack }

public class EnemyAI : MonoBehaviour
{
    public EnemyState currentState;
    private NavMeshAgent agent;
    public Transform player;
    public float sightRange = 10f, attackRange = 2f;
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = EnemyState.Patrol;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Chase:
                Chase();
                break;
            case EnemyState.Attack:
                Attack();
                break;
        }

    }

    void Idle()
    {
        agent.isStopped = true;

    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;
        agent.isStopped = false;

        agent.SetDestination(patrolPoints[currentPatrolIndex].position);

        if (agent.pathPending == false && agent.remainingDistance < 1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }

        CheckForPlayer();
    }

    void Chase()
    {
        agent.SetDestination(player.position);

        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            currentState = EnemyState.Attack;
        }
        else if (Vector3.Distance(transform.position, player.position) > sightRange)
        {
            currentState = EnemyState.Patrol;
        }
    }

    void Attack()
    {
        agent.isStopped = true;

        if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            agent.isStopped = false;
            currentState = EnemyState.Chase;
        }
    }

    void CheckForPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < sightRange)
        {
            currentState = EnemyState.Chase;
        }
    }
}
