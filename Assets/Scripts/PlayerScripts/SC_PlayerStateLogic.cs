using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum PlayerStates
{
    Still,
    Sneaking,
    Walking,
    Running,
    Exhausted,
    BeingHeld,
    Crouching,
    Hiding,
}
public class SC_PlayerStateLogic : MonoBehaviour
{
    private SC_PlayerMovement playerMovement;
    private SC_PlayerHealth playerHealth;

    [Header("Dependencies")]
    public SC_HearingManager_SO hearingManager;

    [Header("Settings")]
    public float maxStamina = 5.0f;
    public float maxDurationHold;
    [ReadOnly] public PlayerStates playerState;

    private float stamina;
    private bool exhausted;
    private bool mashButtonTracker; //The bool which alternates between true and false when mashing buttons/flicking the stick when In a grab
    private float heldTimer;

    //finn
    private Animator animator;

    private SC_EnemyAttack attackingEnemy;

    [HideInInspector] public bool isHiding;
    void Start()
    {
        stamina = maxStamina;
        playerMovement = GetComponent<SC_PlayerMovement>();
        playerHealth = GetComponent<SC_PlayerHealth>();
        //finn
        animator = GetComponentInChildren<Animator>(); 
    }

    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        var input = new Vector3(h, 0, v);
        var inputDir = input.normalized;
        bool moving = false;

        if (isHiding)
            return;

        AnimatePlayer(playerState);

        /* Running Exhaustion Code */
        if (playerState == PlayerStates.Running)
        {
            stamina -= 1 * Time.deltaTime;
            if (stamina < 0) stamina = 0f;
        }
        else
        {
            stamina += 1 * Time.deltaTime;
            if (stamina >= maxStamina)
            {
                stamina = maxStamina;
            }
        }

        if (playerState == PlayerStates.BeingHeld)
        {
            //If using keyboard and if using mouse change it from input to inputdir
            EscapingGrab(inputDir);
            return;
        }

        if (inputDir != Vector3.zero) moving = true;

        playerState = PlayerStates.Walking;

        if (Input.GetKey(KeyCode.RightShift))
        {
            playerState = PlayerStates.Running;
        }

 

        if (stamina <= 0)
        {
            exhausted = true;
        }
        if (stamina >= maxStamina)
        {
            exhausted = false;
        }

        if (exhausted == true)
        {
            playerState = PlayerStates.Exhausted;
        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (inputDir != Vector3.zero)
            {
                playerState = PlayerStates.Sneaking;
            }
            else
            {
                playerState = PlayerStates.Crouching;
            }
            //playerstate = inputDir != Vector3.zero ? PlayerStates.Sneaking : PlayerStates.Hiding:
        }


        //if (!moving) playerState = PlayerStates.Still;

        if (!moving)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerState = PlayerStates.Sneaking;
            }
            else
            {
                playerState = PlayerStates.Still;
            }
        }



        playerMovement.Move(input, inputDir, playerState);

        MakeFootstepSounds(playerState);
        // float movementSpeed = ((running) ? 1 : 0.5f) * inputDir.magnitude;
        // _anim.SetFloat("movementSpeed", movementSpeed, SpeedSmoothTime, Time.deltaTime);

        if (playerState == PlayerStates.Sneaking)
        {
            if (inputDir != Vector3.zero)
            {
                animator.SetFloat("crouch", 1, 0.1f, Time.deltaTime);

            }
            else
            {
                animator.SetFloat("crouch", 0, 0.1f, Time.deltaTime);
            }
        }

    }
    //finns code
    public void AnimatePlayer(PlayerStates currentPlayerState)
    {

        if (playerState == PlayerStates.Sneaking)
        {
            animator.SetBool("mhm", true);
        }
        else
        {
            animator.SetBool("mhm", false);
        }

        if (playerState == PlayerStates.BeingHeld)
        {
            animator.SetBool("grab", true);
        }
        else
        {
            animator.SetBool("grab", false);
        }

        if (playerState == PlayerStates.BeingHeld)
        {
            animator.SetBool("hide", true);
        }
        else
        {
            animator.SetBool("hide", false);
        }



        switch (currentPlayerState)
        {
            case PlayerStates.Still:
                animator.SetFloat("speed", 0, 0.1f, Time.deltaTime);
                break;
            case PlayerStates.Walking:
                animator.SetFloat("speed", 0.5f, 0.1f, Time.deltaTime);
                break;
            case PlayerStates.Running:
                animator.SetFloat("speed", 1, 0.1f, Time.deltaTime);
                break;
            case PlayerStates.BeingHeld:
                break;

            // case PlayerStates.Sneaking:


            // if (inputDir != Vector3.zero)
            //  {
            //   animator.SetFloat("speed", 1, 0.1f, Time.deltaTime);

            //  }
            // else
            //   {
            // animator.SetFloat("speed", 0, 0.1f, Time.deltaTime);
            // }
            //  break;



            //if (playerState == PlayerStates.Sneaking)
            //  {
            //      animator.SetBool("mhm", true);
            //  }
            //  else
            //  {
            //      animator.SetBool("mhm", false);
            //  }



            // if (case PlayerStates.Sneaking:)
            //{
            //    animator.SetBool("mhm", true);
            //  }
            //  else
            //   {
            //      animator.SetBool("mhm", false);
            //   }
            //   break;

            case PlayerStates.Exhausted:
                break;

        }
    }
    private void MakeFootstepSounds(PlayerStates playerState)
    {
        //Decide wether or not to make a sound or nah
        switch (playerState)
        {
            case PlayerStates.Walking:
                hearingManager.MakeASound(this.transform.position, 0.5f);
                break;
            case PlayerStates.Running:
                hearingManager.MakeASound(this.transform.position, 1f);
                break;
            default: break;
        }
    }

    private void EscapingGrab(Vector3 inputDir)
    {
        playerHealth.BeingHeld();
        heldTimer += Time.deltaTime;

        if (mashButtonTracker ? inputDir.magnitude < .9f : inputDir.magnitude > .9f)
        {
            mashButtonTracker = !mashButtonTracker;

            heldTimer += .25f;
        }

        if (heldTimer >= maxDurationHold)
        {
            playerState = PlayerStates.Walking;

            StartCoroutine(attackingEnemy.PlayerEscaped());
        }
    }

    public void Grabbed(SC_EnemyAttack attackingEnemy)
    {
        this.attackingEnemy = attackingEnemy;

        playerState = PlayerStates.BeingHeld;
        heldTimer = 0;
    }
  
}

