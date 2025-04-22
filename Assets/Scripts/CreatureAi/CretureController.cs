using UnityEngine;
using UnityEngine.AI;


public class CretureController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float startWaitTime = 4f;
    public float timeToRotate = 2f;
    public float walkSpeed = 6f;
    public float runSpeed = 9f;

    public float viewRadius = 15f;
    public float viewAngle = 90f;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float meshResolution = 1f;
    public int edgeIteration = 4;
    public float edgeDistance = 0.5f;

    public Transform[] waypoints;
    int curWaypointIndex;

    Vector3 lastPlayerPos = Vector3.zero;
    Vector3 playerPos;

    float waitTime;
    float rotateTime;
    bool playerInRange;
    bool playerNear;
    bool isRoaming;
    bool caughtPlayer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerPos = Vector3.zero;
        isRoaming = true;
        caughtPlayer = false;
        playerInRange = false;
        waitTime = startWaitTime;
        rotateTime = timeToRotate;

        curWaypointIndex = 0;
        agent = GetComponent<NavMeshAgent>();

        agent.isStopped = false;
        agent.speed = walkSpeed;
        agent.SetDestination(waypoints[curWaypointIndex].position);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CaughtPlayer()
    {
        caughtPlayer = true;
    }

    public void LookingPlayer(Vector3 player)
    {
        agent.SetDestination(player);
        if (Vector3.Distance(transform.position, player) <= 0.3f)
        {
            if (waitTime <= 0)
            {
                playerNear = false;
                Move(walkSpeed);
                agent.SetDestination(waypoints[curWaypointIndex].position);
                waitTime = startWaitTime;
                rotateTime = timeToRotate;
            }
            else
            {
                Stop();
                waitTime -= Time.deltaTime;
            }
        }
    }

    public void Move(float speed)
    {
        agent.isStopped = false;
        agent.speed = speed;
    }

    public void Stop()
    {
        agent.isStopped = true;
        agent.speed = 0;
    }
    public void NextPoint()
    {
        curWaypointIndex = (curWaypointIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[curWaypointIndex].position);
    }

    void EnviormentView()
    {
        Collider[] playersInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        for (int i = 0; i < playersInRange.Length; i++)
        {
            Transform player = playersInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float disToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, disToPlayer, obstacleMask))
                {
                    playerInRange = true;
                    isRoaming = false;
                }
                else
                {
                    playerInRange = false;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                playerInRange = false;
            }
            if (playerInRange)
            {
                playerPos = player.transform.position;
            }
        }
    }
    private void Roaming()
    {
        if (playerNear)
        {
            if(rotateTime <= 0)
            {
                Move(walkSpeed);
                LookingPlayer(lastPlayerPos);
            }
            else
            {
                Stop();
                rotateTime -= Time.deltaTime;
            }
        }
        else
        {
            playerInRange = false;
            lastPlayerPos = Vector3.zero;
            agent.SetDestination(waypoints[curWaypointIndex].position);
            if(agent.remainingDistance <= agent.stoppingDistance)
            {
                if(waitTime <= 0)
                {
                    NextPoint();
                    Move(walkSpeed);
                    waitTime = startWaitTime;
                }
                else
                {
                    Stop();
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }
}
