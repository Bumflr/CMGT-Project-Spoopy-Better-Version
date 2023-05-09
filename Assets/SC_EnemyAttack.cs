using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_EnemyAttack : MonoBehaviour
{
    public float attackRadius;
    public float secondsTilStriking = 0.5f;
    public Transform attackingPointOffset;
    public LayerMask whatIsPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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

               // GameStateManager.Instance.SetState(GameState.GameOver);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackingPointOffset.position, attackRadius);
    }
}
