using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum EnemyStates
{
    Still,
    Patrolling,
    Chasing,
    HoldingPlayer,
    Stunned
}


public class EnemyAgentControl : MonoBehaviour
{
    [ReadOnly] public EnemyStates enemyState;
    public NavMeshAgent navMeshAgent;               //  Nav mesh agent component

    public float startWaitTime = 4;                 //  Wait time of every action
    public float timeToRotate = 2;                  //  Wait time when the enemy detect near the player without seeing
    public float speedWalk = 6;                     //  Walking speed, speed in the nav mesh agent
    public float speedRun = 9;                      //  Running speed
    
    public float hearRadius = 45;
    public float viewRadius = 15;                   //  Radius of the enemy view
    public float viewAngle = 90;                    //  Angle of the enemy view
    public LayerMask playerMask;                    //  To detect the player with the raycast
    public LayerMask obstacleMask;                  //  To detect the obstacles with the raycast
    
   // public Transform centrePoint;                   //centre of the area the agent wants to move around in, instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area
 
    public Transform[] waypoints;                   //  All the waypoints where the enemy patrols
    int m_CurrentWaypointIndex;                     //  Current waypoint where the enemy is going to
 
    Vector3 playerLastPosition = Vector3.zero;      //  Last position of the player when was near the enemy
 
    float waitTime;                               //  Variable of the wait time that makes the delay
    float m_TimeToRotate;                           //  Variable of the wait time to rotate when the player is near that makes the delay
    
    GameObject m_Player;


    void Start()
    {
        enemyState = EnemyStates.Patrolling;

       // m_PlayerHeard = false;
        waitTime = startWaitTime;                 //  Set the wait time variable that will change
        m_TimeToRotate = timeToRotate;
 
        m_CurrentWaypointIndex = 0;                 //  Set the initial waypoint
        navMeshAgent = GetComponent<NavMeshAgent>();
 
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;             //  Set the navemesh speed with the normal speed of the enemy
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);    //  Set the destination to the first waypoint

        m_Player = GameObject.FindGameObjectWithTag("Player");
    }

  
    private void Update()
    {
        //  Check whether or not the player is in the enemy's field of vision
        if (EnviromentView())
        {
            enemyState = EnemyStates.Chasing;                                          
        }
        else
        {
            enemyState = EnemyStates.Patrolling;     
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


        switch (enemyState)
        {
            case EnemyStates.Still:
                break;
            case EnemyStates.Chasing:
                Chasing();
                break;
            case EnemyStates.Patrolling:
                Patroling();


                break;
                default: break;
        }

    }
    bool EnviromentView()
    {
        bool playerInRange = false;   //  If the player is in range of vision, state of chasing

        Collider[] playerCollider = Physics.OverlapSphere(transform.position, viewRadius, playerMask);   //  Make an overlap sphere around the enemy to detect the playermask in the view radius

        for (int i = 0; i < playerCollider.Length; i++)
        {
            Transform player = playerCollider[i].transform;

            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);          //  Distance of the enemy and the player
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    playerInRange = true; //  The player has been seeing by the enemy and then the nemy starts to chasing the player
                }
                else
                {
                    playerInRange = false;
                }
            }

            if (playerInRange)
            {
                /*
                 *  If the enemy no longer sees the player, then the enemy will go to the last position that has been registered
                 * */
                playerLastPosition = player.transform.position;       //  Save the player's current position if the player is in range of vision
            }
        }

        return playerInRange;
    }

    private void Chasing()
    {
        //  The enemy is chasing the player
        playerLastPosition = Vector3.zero;          //  Reset the player near position

        Move(speedRun);
        navMeshAgent.SetDestination(m_Player.transform.position);          //  set the destination of the enemy to the player location

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)    //  Control if the enemy arrive to the player location
        {
            if (waitTime <= 0 || Vector3.Distance(transform.position, m_Player.transform.position) >= 6f)
            {
                //  Check if the enemy is not near to the player, returns to patrol after the wait time delay
                enemyState = EnemyStates.Patrolling;

                Move(speedWalk);
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
 
    private void Patroling()
    {
        playerLastPosition = Vector3.zero;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);    //  Set the enemy destination to the next waypoint
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            //  If the enemy arrives to the waypoint position then wait for a moment and go to the next
            if (waitTime <= 0)
            {
                NextPoint();
                Move(speedWalk);
                waitTime = startWaitTime;
            }
            else
            {
                Stop();
                waitTime -= Time.deltaTime;
            }
        }
    }

    public void GoingToLastKnownPosition()
    {
        //blab lae do this AND THEN DO THIS AND THEN IF THIS IS NOT THIS, THEN SEND THE CODE 
    }

    public void NextPoint()
    {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }
 
    void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }
 
    void Move(float speed)
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