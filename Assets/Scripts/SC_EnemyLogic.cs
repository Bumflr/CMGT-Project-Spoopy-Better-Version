using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class SC_EnemyLogic : MonoBehaviour
{
    [Header("Dependencies")]
    public SC_EnemyMove move;
    public SC_HearingManager_SO hearingManagerScriptableObject;
    public Animator animator;
    public SC_EnemyVisibility visibility;

    [Header("Settings")]
    [ReadOnly, SerializeField] private EnemyStates enemyState;

    public float hearRadius = 45;
    public float viewRadius = 15;                   //  Radius of the enemy view
    public float viewAngle = 90;                    //  Angle of the enemy view
    public LayerMask playerMask;                    //  To detect the player with the raycast
    public LayerMask obstacleMask;                  //  To detect the obstacles with the raycast

    public bool isSpiderEnemy;

    int isIdleHash;
    int isWalkingHash;
    int isRunningHash;
    int isLightWalkingHash;
    int isGrabbing;

    GameObject m_Player;
    Vector3 currentTargetPosition;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        visibility = GetComponent<SC_EnemyVisibility>();

        isIdleHash = Animator.StringToHash("isIdle");
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isLightWalkingHash = Animator.StringToHash("isLightWalking");
        isGrabbing = Animator.StringToHash("isGrabbing");

        SetEnemyState(EnemyStates.Patrolling);

        hearingManagerScriptableObject.hearingEvent.AddListener(ListenToSounds);

        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;

        if (isSpiderEnemy)
        {
            m_Player = GameObject.FindGameObjectWithTag("Player");

            currentTargetPosition = m_Player.transform.position;
        }
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        if (newGameState != GameState.Gameplay)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;

        hearingManagerScriptableObject.hearingEvent.RemoveListener(ListenToSounds);
    }

    private void Update()
    {
        //  Check whether or not the player is in the enemy's field of vision

        if ((isSpiderEnemy || EnemyViewCone()) && enemyState != EnemyStates.Chasing && enemyState != EnemyStates.HoldingPlayer && enemyState != EnemyStates.Stunned)
        {
            SetEnemyState(EnemyStates.Chasing);

            SoundManager.PlaySound(SoundManager.Sound.GhostSound);
        }

        if (visibility.BeingLit && enemyState == EnemyStates.Chasing)
        {
            SetEnemyState(EnemyStates.BeingLit);
        }

        switch (enemyState)
        {
            case EnemyStates.Still:
                SetAnimations(true, false, false);
                break;

            case EnemyStates.Chasing:
                currentTargetPosition = m_Player.transform.position;

                if ((!isSpiderEnemy && !EnemyViewCone()) && Vector3.Distance(transform.position, m_Player.transform.position) >= 5f)
                {
                    //You lost the enemy
                    SetEnemyState(EnemyStates.Patrolling);
                }
                SetAnimations(false, false, true);
                break;
            case EnemyStates.BeingLit:
                currentTargetPosition = isSpiderEnemy ? this.transform.position - m_Player.transform.position : m_Player.transform.position;

                if ((!isSpiderEnemy && !EnemyViewCone()) && Vector3.Distance(transform.position, m_Player.transform.position) >= 5f)
                {
                    //You lost the enemy
                    SetEnemyState(EnemyStates.Patrolling);
                }
                SetAnimations(false, false, true);
                break;
            case EnemyStates.Investigating:
            case EnemyStates.Patrolling:
                SetAnimations(false, true, false);
                break;
            case EnemyStates.HoldingPlayer:

                if (isSpiderEnemy)
                    return;
                animator.SetBool(isGrabbing, true);
                break;
         
            default: break;
        }

        if (enemyState != EnemyStates.HoldingPlayer && enemyState != EnemyStates.Stunned)
        {
            move.SetMoveAndState(enemyState, currentTargetPosition);
        }

    }

    public void SetAnimations(bool idle, bool walk, bool run)
    {
        if (isSpiderEnemy)
            return;

        animator.SetBool(isIdleHash, idle);
        animator.SetBool(isWalkingHash, walk);
        animator.SetBool(isRunningHash, run);
    }

    public void ReachedPoint()
    {
        switch (enemyState) 
        {
            case EnemyStates.Still:
                SetEnemyState(EnemyStates.Still);
                break;
            case EnemyStates.Patrolling:
                SetEnemyState(EnemyStates.Still);
                break;
            case EnemyStates.Investigating:
                SetEnemyState(EnemyStates.Patrolling);
                break;
            case EnemyStates.Chasing:

                if (m_Player.GetComponent<SC_PlayerStateLogic>().isHiding)
                {
                    Debug.Log("GETCHO ASS OUTTA THERE I SAW YO NON JORDAN HAVING ASS");

                }
                break;
            case EnemyStates.BeingLit:

                if (m_Player.GetComponent<SC_PlayerStateLogic>().isHiding)
                {
                    Debug.Log("GETCHO ASS OUTTA THERE I SAW YO NON JORDAN HAVING ASS");

                }
                break;
            default: break;
        }
    }

    public void SetEnemyState(EnemyStates enemyState)
    {
        this.enemyState = enemyState;
    }
   
    bool EnemyViewCone()
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
                    m_Player = player.gameObject;


 
                    playerInRange = !m_Player.GetComponent<SC_PlayerStateLogic>().isHiding; //  The player has been seeing by the enemy and then the nemy starts to chasing the player
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
                //playerLastPosition = player.transform.position;       //  Save the player's current position if the player is in range of vision
            }
        }

        return playerInRange;
    }
    private void ListenToSounds(Vector3 pos, float volume)
    {
        var hearRadius = this.hearRadius * volume;

        if (Vector3.Distance(transform.position, pos) <= hearRadius)
        {
            //Debug.Log($"Heard a sound at: {pos.ToString()}, by: {this.gameObject.name}");

            if (enemyState != EnemyStates.Chasing && enemyState != EnemyStates.BeingLit && enemyState != EnemyStates.Stunned && enemyState != EnemyStates.HoldingPlayer && !isSpiderEnemy)
            {
                currentTargetPosition = pos;
                SetEnemyState(EnemyStates.Investigating);
            }
        }
    }

}