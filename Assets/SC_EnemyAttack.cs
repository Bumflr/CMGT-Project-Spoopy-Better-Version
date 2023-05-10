using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_EnemyAttack : MonoBehaviour
{
    public float attackRadius;
    public float secondsTilStriking = 0.5f;
    public Transform attackingPointOffset;
    public LayerMask whatIsPlayer;

    public bool holdingPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !holdingPlayer)
        {
            //Caught Player
            //Now prepare an attack
            //Play like a charging sound effect here or something
            Debug.Log("Grabbing!");
            StartCoroutine(AttackPlayer(other));
        }
    }

    private IEnumerator AttackPlayer(Collider playerCollider)
    {
        yield return new WaitForSeconds(secondsTilStriking);
        //Play like a growl sound effect here or something

        Collider[] test = Physics.OverlapSphere(attackingPointOffset.position, attackRadius, whatIsPlayer);

        foreach(Collider b in test) 
        {
            if (b == playerCollider)
            {
                b.GetComponent<SC_PlayerLogic>().Grabbed();



                //After grabbing the player, set the enemy state to something like grabbing , and stop the enemy of doing anything else  
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackingPointOffset.position, attackRadius);
    }
}
