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
    Stunned,
    BeingLit
}


public class SC_EnemyMove : MonoBehaviour
{
    [Header("Dependencies")]
    public NavMeshAgent navMeshAgent;               //  Nav mesh agent component
    public SC_EnemyLogic enemyLogic;

    [Header("Settings")]
    public float startWaitTime = 4;                 //  Wait time of every action
    public float timeToRotate = 2;                  //  Wait time when the enemy detect near the player without seeing
    public float speedWalk = 6;                     //  Walking speed, speed in the nav mesh agent
    public float speedRun = 9;                      //  Running speed

    int amountOfWayPoints = 3;
    Vector3[] waypoints;                   //  All the waypoints where the enemy patrols
    int m_CurrentWaypointIndex;                     //  Current waypoint where the enemy is going to

    float waitTime = 4;                               //  Variable of the wait time that makes the delay
    float m_TimeToRotate;                           //  Variable of the wait time to rotate when the player is near that makes the delay

    void Start()
    {
       // m_PlayerHeard = false;
        waitTime = startWaitTime;                 //  Set the wait time variable that will change
        m_TimeToRotate = timeToRotate;
 
        m_CurrentWaypointIndex = 0;                 //  Set the initial waypoint
        navMeshAgent = GetComponent<NavMeshAgent>();
 
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;             //  Set the navemesh speed with the normal speed of the enemy

        waypoints = new Vector3[1];

        waypoints[0] = this.transform.position;
        /*for (int i = 0; i < amountOfWayPoints; i++)
        {
            RandomPoint(this.transform.position, 5f, out waypoints[i]);

            Debug.Log($"{i} + {waypoints[i]}");
        }*/

        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex]);    //  Set the destination to the first waypoint

    }

    public void SetMoveAndState(EnemyStates enemyState, Vector3 target)
    {
        Vector3 targetPosition = Vector3.zero;

        switch (enemyState)
        {
            case EnemyStates.Still:
                return;
            case EnemyStates.Chasing:
                SetSpeed(speedRun);
                targetPosition = target;
                break;
            case EnemyStates.BeingLit:
                SetSpeed(speedWalk);
                targetPosition = target;
                break;
            case EnemyStates.Patrolling:
                SetSpeed(speedWalk);
                targetPosition = waypoints[m_CurrentWaypointIndex];
                break;
            case EnemyStates.Investigating:
                SetSpeed(speedWalk);
                targetPosition = target;
                break;
                default: break;
        }

        Move(targetPosition);
    }

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

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            Debug.DrawRay(randomPoint, hit.position);
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
 
   
}