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
}

public class SC_PlayerLogic : MonoBehaviour
{
    //Playerlogic script that decides what state the player is in 
    //As well as the inputs (for now)
    private SC_PlayerMovement playerMovement;
    private SC_PlayerHealth playerHealth;

    public bool Exhausted;
    public bool Moving;
    public float Stamina;
    public float MaxStamina = 5.0f;

    public PlayerStates playerState;

    public float SpeedSmoothTime = 0.1f;

    public float maxDurationHold;
    private bool mashButtonTracker; //The bool which alternates between true and false when mashing buttons/flicking the stick when In a grab
    private float heldTimer;

    [HideInInspector] public bool isHiding;
    void Start()
    {
        Stamina = MaxStamina;
        playerMovement = GetComponent<SC_PlayerMovement>();
        playerHealth = GetComponent<SC_PlayerHealth>();
    }

    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        var input = new Vector3(h, 0, v);
        var inputDir = input.normalized;

        if (isHiding)
            return;

        if (playerState == PlayerStates.BeingHeld)
        {
            //If using keyboard and if using mouse change it from input to inputdir
            EscapingGrab(inputDir);
            return;
        }

        if (inputDir != Vector3.zero) Moving = true;

        playerState = PlayerStates.Walking;

        if (Input.GetKey(KeyCode.RightShift))
        {
            playerState = PlayerStates.Running;
        }

        /* Running Code */
        if (playerState == PlayerStates.Running)
        {
            Stamina -= 1 * Time.deltaTime;
            if (Stamina < 0) Stamina = 0f;
        }
        else
        {
            Stamina += 1 * Time.deltaTime;
            if (Stamina >= MaxStamina)
            {
                Stamina = MaxStamina;
            }
        }

        if (Stamina <= 0)
        {
            Exhausted = true;
        }
        if (Stamina >= MaxStamina)
        {
            Exhausted = false;
        }

        if (Exhausted == true)
        {
            playerState = PlayerStates.Exhausted;
        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerState = PlayerStates.Sneaking;
        }


        if (!Moving) playerState = PlayerStates.Still;


        playerMovement.Move(input, inputDir, playerState);
   

        // float movementSpeed = ((running) ? 1 : 0.5f) * inputDir.magnitude;
        // _anim.SetFloat("movementSpeed", movementSpeed, SpeedSmoothTime, Time.deltaTime);
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
        }
    }

    public void Grabbed()
    {
        playerState = PlayerStates.BeingHeld;
        heldTimer = 0;
    }
  
}

