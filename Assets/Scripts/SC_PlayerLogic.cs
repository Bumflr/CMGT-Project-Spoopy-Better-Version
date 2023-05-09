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
    Exhausted
}

public class SC_PlayerLogic : MonoBehaviour
{
    private SC_PlayerMovement playerMovement;

    public bool Exhausted;
    public bool Moving;
    public float Stamina;
    public float MaxStamina = 5.0f;

    public PlayerStates playerState;

    public float SpeedSmoothTime = 0.1f;

    [HideInInspector] public bool isHiding;
    void Start()
    {
        Stamina = MaxStamina;
        playerMovement = GetComponent<SC_PlayerMovement>();
    }

    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        var input = new Vector3(h, 0, v);
        var inputDir = input.normalized;

        if (isHiding)
            return;

        if (inputDir != Vector3.zero) Moving = true;

        //Right now all of the player's abilities are in 1 script which might not be the most smart thing to do, but whatever it's a prototype

        playerState = PlayerStates.Walking;

        if (Input.GetKey(KeyCode.RightShift))
        {
            playerState = PlayerStates.Running;
        }

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
  
  
}

