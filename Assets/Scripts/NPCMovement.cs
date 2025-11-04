using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;

    private NavMeshAgent agent;
    private float timer;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        timer = wanderTimer;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }

        // changes between idle and walking animation, uses same animator from our player model
        if (animator != null)
        {
            bool isMoving = agent.velocity.magnitude > 0.1f;
            animator.SetBool("isWalking", isMoving);
        }
    }

    //useful function from a video guide that basically scans the area around the npc 
    // in a circle and finds a random point to walk to
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * dist;
        randomDirection += origin;
        NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, dist, layermask);
        return navHit.position;
    }
}
