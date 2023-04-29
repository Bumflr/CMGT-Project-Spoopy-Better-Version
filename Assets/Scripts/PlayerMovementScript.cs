using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum PlayerStates
{
    Sneaking,
    Walking,
    Running
}

public class PlayerMovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float SneakSpeed = 0.3f;
    public float WalkSpeed = 1.0f;
    public float RunSpeed = 2.0f;
    public float RotationSpeed = 5.0f;
    public float RotationSmoothTime = 0.2f;
    public float Gravity = -12f;
    public float JumpHeight = 1.0f;

    public float turnSpeed;
    [Range(0.0f, 1.0f)]
    public float lerpValueRotation;

    private PlayerStates playerState;

    [Tooltip("Curve that handles movement speed of the dodge, [X axis = duration, Y axis = strength]")]
    public AnimationCurve DodgeCurve;

    private bool dodgeStart;
    private float dodgeTimer;
    private float durationOfDodge;
    private float dodgeModifier;

    float velocityY;
    float rotationVelocityTime;

    public float SpeedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    public Camera _cam;
    private CharacterController _cController;
    void Start()
    {
        //_cam = FindObjectOfType<Camera>();
        _cController = GetComponent<CharacterController>();

        durationOfDodge = DodgeCurve[DodgeCurve.length - 1].time;
    }


    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        var input = new Vector3(h, 0, v);
        var inputDir = input.normalized;

        //**MOUSE ROTATION**
        /*if (!Input.GetKey(KeyCode.L))
        {
            Ray cameraRay = _cam.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.cyan);

                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
        }*/


        //Right now all of the player's abilities are in 1 script which might not be the most smart thing to do, but whatever it's a prototype

        var targetSpeed = 1f;

        dodgeModifier = 1f;

       /* if (Input.GetButtonDown("Jump") && !dodgeStart)
        {
            dodgeStart = true;
            dodgeTimer = 0;
        }

        if (dodgeStart && dodgeTimer < durationOfDodge)
        {
            dodgeTimer += Time.deltaTime;

            dodgeModifier = DodgeCurve.Evaluate(dodgeTimer);
        }
        else
        {
            dodgeStart = false;
            dodgeModifier = 1f;
        }*/

        playerState = PlayerStates.Walking;

        if (input.magnitude > 0.9f) playerState = PlayerStates.Running;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerState = PlayerStates.Sneaking;
        }
        

        switch (playerState)
        {
            case PlayerStates.Sneaking:
                targetSpeed = SneakSpeed * inputDir.magnitude * dodgeModifier;
                break;
            case PlayerStates.Walking:
                targetSpeed = WalkSpeed * inputDir.magnitude * dodgeModifier;
                break;
            case PlayerStates.Running:
                targetSpeed = RunSpeed  * inputDir.magnitude * dodgeModifier;
                break;
        }

        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, SpeedSmoothTime);

        velocityY += Time.deltaTime * Gravity;
        Vector3 moveDir = _cam.transform.right * inputDir.x + _cam.transform.forward * inputDir.z;
        var velocity = new Vector3(moveDir.x, 0, moveDir.z) * currentSpeed + Vector3.up * velocityY;


        _cController.Move(velocity * Time.deltaTime);

        if (_cController.isGrounded)
        {
            velocityY = 0;
            //_anim.SetBool("jumping", false);

        }

        //**Keypress Based Rotation
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if (inputDir.magnitude > 0.9f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(velocity), lerpValueRotation);
            }
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up * -turnSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
        }

        // float movementSpeed = ((running) ? 1 : 0.5f) * inputDir.magnitude;
        // _anim.SetFloat("movementSpeed", movementSpeed, SpeedSmoothTime, Time.deltaTime);
    }


    void Jump()
    {
        if (_cController.isGrounded)
        {
            //_anim.SetBool("jumping", true);
            var jumpVelocity = Mathf.Sqrt(-2 * Gravity * JumpHeight);
            velocityY = jumpVelocity;
        }
    }


    //public void GrabLedge(Collider other)
    //{

    //    var rot = Quaternion.LookRotation(other.transform.forward);
    //    transform.rotation = rot;
    //    transform.localPosition += new Vector3(other.transform.localPosition.x, 0f, 0f);
    //    _anim.SetBool("HangClimb", true);
    //}


}

