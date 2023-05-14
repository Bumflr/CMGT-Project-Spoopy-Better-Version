using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SC_EnemyLogic : MonoBehaviour
{
    public EnemyStates enemyState;

    public float viewRadius = 15;                   //  Radius of the enemy view
    public float viewAngle = 90;                    //  Angle of the enemy view

    public LayerMask playerMask;                    //  To detect the player with the raycast
    public LayerMask obstacleMask;                  //  To detect the obstacles with the raycast

    /* private void Update()
    {
        if (IsPlayerInView())
        {
            enemyState = EnemyStates.Chasing;
        }
    }

    bool IsPlayerInView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);   //  Make an overlap sphere around the enemy to detect the playermask in the view radius

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;

            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);          //  Distance of the enemy and the player
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    return true;             //  The player has been seeing by the enemy and then the nemy starts to chasing the player
                   // m_IsPatrol = false;                 //  Change the state to chasing the player
                }
                else
                {
                    /*
                     *  If the player is behind a obstacle the player position will not be registered
                     * */
                   // m_IsPatrol = true;
                   // return false;
               // }
            //}

           /* if (Vector3.Distance(transform.position, player.position) > viewRadius)
{
    /*
     *  If the player is further than the view radius, then the enemy will no longer keep the player's current position.
     *  Or the enemy is a safe zone, the enemy will no chase
     * 
   // return false;                //  Change the sate of chasing
}

            /*if (m_playerInRange)
            {
                /*
                 *  If the enemy no longer sees the player, then the enemy will go to the last position that has been registered
                 * */
               /* m_PlayerPosition = player.transform.position;       //  Save the player's current position if the player is in range of vision
            }
        }
    }*/
   
}
