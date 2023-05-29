using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum EnemyStates
{
    Still,
    Patrolling,
    Chasing,
    Investigating,
    HoldingPlayer,
    Stunned
}


public class SC_EnemyMove : MonoBehaviour
{
    Animator animator;
    int isIdleHash;
    int isWalkingHash;
    int isRunningHash;
    int isLightWalkingHash;

    [Header("Dependencies")]
    public NavMeshAgent navMeshAgent;               //  Nav mesh agent component
    public SC_EnemyLogic enemyLogic;

    [Header("Settings")]
    public float startWaitTime = 4;                 //  Wait time of every action
    public float timeToRotate = 2;                  //  Wait time when the enemy detect near the player without seeing
    public float speedWalk = 6;                     //  Walking speed, speed in the nav mesh agent
    public float speedRun = 9;                      //  Running speed
   
    public Transform[] waypoints;                   //  All the waypoints where the enemy patrols
    int m_CurrentWaypointIndex;                     //  Current waypoint where the enemy is going to
 
 
    float waitTime = 4;                               //  Variable of the wait time that makes the delay
    float m_TimeToRotate;                           //  Variable of the wait time to rotate when the player is near that makes the delay
    

    void Start()
    {
        animator = GetComponent<Animator>();

        isIdleHash = Animator.StringToHash("isIdle");
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isLightWalkingHash = Animator.StringToHash("isLightWalking");
       // m_PlayerHeard = false;
        waitTime = startWaitTime;                 //  Set the wait time variable that will change
        m_TimeToRotate = timeToRotate;
 
        m_CurrentWaypointIndex = 0;                 //  Set the initial waypoint
        navMeshAgent = GetComponent<NavMeshAgent>();
 
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;             //  Set the navemesh speed with the normal speed of the enemy
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);    //  Set the destination to the first waypoint
    }

  
    public void SetMoveAndState(EnemyStates enemyState, Vector3 target)
    {
        Vector3 targetPosition = Vector3.zero;

        switch (enemyState)
        {
            case EnemyStates.Still:
                animator.SetBool("isIdle", true);
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", false);
                return;

            case EnemyStates.Chasing:
                animator.SetBool("isIdle", false);
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalking", false);
                SetSpeed(speedRun);
                targetPosition = target;

                break;
            case EnemyStates.Patrolling:
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
                animator.SetBool("isIdle", false);
                SetSpeed(speedWalk);
                targetPosition = waypoints[m_CurrentWaypointIndex].position;

                break;
            case EnemyStates.Investigating:
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
                animator.SetBool("isIdle", false);
                SetSpeed(speedWalk);
                targetPosition = target;

                break;
                default: break;
        }


        Move(targetPosition);
    }

    /* if (!m_IsPatrol)
     {

     }
     else
     {
         //SoundManager.PlaySound(SoundManager.Sound.GhostIdle, this.transform.position);

         if (playerNear)
         {
             //  Check if the enemy detect near the player, so the enemy will move to that position
             if (m_TimeToRotate <= 0)
             {
                 SoundManager.PlaySound(SoundManager.Sound.GhostChase, this.transform.position);

                 Move(speedWalk);
                 //LookingPlayer(playerLastPosition);
             }
             else
             {
                 //  The enemy wait for a moment and then go to the last player position
                 Stop();
                 m_TimeToRotate -= Time.deltaTime;
             }
         }
         else
         {
         }
     }*/

    private void Move(Vector3 targetPosition)
    {
        navMeshAgent.SetDestination(targetPosition);    //  Set the enemy destination to the next waypoint

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            //  If the enemy arrives to the target position then wait for a moment and go to the next
            if (0f <= waitTime)
            {
                // NextPoint();

                enemyLogic.ReachedPoint();

                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;

                SetSpeed(speedWalk);
                waitTime = startWaitTime;
            }
            else
            {
                Stop();
                waitTime -= Time.deltaTime;
            }
        }
    }

    /*private void Chasing()
    {
        //  The enemy is chasing the player

        navMeshAgent.SetDestination(m_Player.transform.position);          //  set the destination of the enemy to the player location

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)    //  Control if the enemy arrive to the player location
        {
            if (waitTime <= 0 || Vector3.Distance(transform.position, m_Player.transform.position) >= 6f)
            {
                //  Check if the enemy is not near to the player, returns to patrol after the wait time delay
                enemyLogic.SetEnemyState(EnemyStates.Patrolling);

                SetSpeed(speedWalk);
                m_TimeToRotate = timeToRotate;
                waitTime = startWaitTime;
                //randomMovement();
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
            else
            {
                if (Vector3.Distance(transform.position, m_Player.transform.position) >= 2.5f)
                    Stop();
                //  Wait if the current position is not the player position

                waitTime -= Time.deltaTime;
            }
        }
    }
 
    /*private void Patroling()
    {
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);    //  Set the enemy destination to the next waypoint
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            //  If the enemy arrives to the waypoint position then wait for a moment and go to the next
            if (waitTime <= 0)
            {
                NextPoint();
                SetSpeed(speedWalk);
                waitTime = startWaitTime;
            }
            else
            {
                Stop();
                waitTime -= Time.deltaTime;
            }
        }
    }*/

    public void GoingToLastKnownPosition()
    {
        //blab lae do this AND THEN DO THIS AND THEN IF THIS IS NOT THIS, THEN SEND THE CODE 
    }

    void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }
 
    void SetSpeed(float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }

   /* void randomMovement()
    {
        Vector3 point;
        if (RandomPoint(centrePoint.position, range, out point))    //  pass in our centre point and radius of area
        {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);     //  so you can see with gizmos
                navMeshAgent.SetDestination(point);
        }
    }*/

    /*void LookingPlayer(Vector3 player)
    {
        navMeshAgent.SetDestination(player);
        if (Vector3.Distance(transform.position, player) <= 0.3)
        {
            if (m_WaitTime <= 0)
            {
                Move(speedWalk);
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                m_WaitTime = startWaitTime;
                m_TimeToRotate = timeToRotate;
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }*/

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        { 
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
 
   
}