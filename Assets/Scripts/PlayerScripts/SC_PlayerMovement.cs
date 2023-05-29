using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerMovement : MonoBehaviour
{
    [Header("Dependencies")]
    public Camera _cam;
    private CharacterController _cController;
    [Header("Settings")]
    public float SneakSpeed = 0.3f;
    public float WalkSpeed = 1.0f;
    public float RunSpeed = 2.0f;
    public float ExhaustSpeed = 0.5f;
    [Space(20)]
    public float turnSpeed;
    public float SpeedSmoothTime = 0.1f;
    public float lerpValueRotation;
    public LayerMask layerMask;


    float velocityY;
    Vector3 savedVelocity;

    float Gravity = -12f;
    float speedSmoothVelocity;
    float currentSpeed;


    // Start is called before the first frame update
    void Awake()
    {
        _cController = GetComponent<CharacterController>();
        savedVelocity = Vector3.forward;
    }

    public void Move(Vector3 input, Vector3 inputDir, PlayerStates state)
    {
        var targetSpeed = 1f;

        switch (state)
        {
            case PlayerStates.Sneaking:
                targetSpeed = SneakSpeed * inputDir.magnitude;
                break;
            case PlayerStates.Walking:
                targetSpeed = WalkSpeed * inputDir.magnitude;
                break;
            case PlayerStates.Running:
                targetSpeed = RunSpeed * inputDir.magnitude;
                break;
            case PlayerStates.Exhausted:
                targetSpeed = ExhaustSpeed * inputDir.magnitude;
                break;

        }

        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, SpeedSmoothTime);

        velocityY += Time.deltaTime * Gravity;
        Vector3 moveDir = _cam.transform.right * inputDir.x + _cam.transform.forward * inputDir.z;
        moveDir = moveDir.normalized;


        var velocity = new Vector3(moveDir.x, 0, moveDir.z) * currentSpeed;


        if (_cController.isGrounded)
        {
            velocityY = 0;
        }
        if (input.magnitude > .45f)
        {
            savedVelocity = velocity;
        }

        _cController.Move((velocity + Vector3.up * velocityY) * Time.deltaTime);

        Rotate(savedVelocity);
    }

    void Rotate(Vector3 savedVelocity)
    {
        //**Keypress Based Rotation

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(savedVelocity, Vector3.up), lerpValueRotation * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up * -turnSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
        }
        



        //**MOUSE ROTATION**
        /*if (!Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log(new Vector3(Input.mousePosition.normalized.x, Input.mousePosition.normalized.y, Input.mousePosition.normalized.z));

            Ray cameraRay = _cam.ViewportPointToRay(new Vector3(Input.mousePosition.normalized.x, Input.mousePosition.normalized.y));

            float rayLength = 99;
            RaycastHit hitInfo;

            if (Physics.Raycast(cameraRay, out hitInfo, rayLength, layerMask))
            {
                Debug.DrawLine(cameraRay.origin, hitInfo.point, Color.cyan);

                transform.LookAt(new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z));
            }
        }*/
    }
}