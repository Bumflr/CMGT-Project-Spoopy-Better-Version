using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStates
{
    Still,
    Patrolling,
    Chasing,
    HoldingPlayer,
    Stunned
}



public class SC_EnemyLogic : MonoBehaviour
{
    public EnemyStates enemyState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
