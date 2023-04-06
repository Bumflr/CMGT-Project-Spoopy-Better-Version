using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLightFOVCheck : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;
    
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public Toggle testToggle;

    public bool canSeePlayer { get; private set; }

    private void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Ghost");

        if (TryGetComponent<Light>(out Light lighting))
        {
            Debug.Log(lighting);

            if (lighting.type == LightType.Spot)
            {
                radius = lighting.range;
                angle = lighting.spotAngle;
            }
            else
            {
                Debug.Log("A Light Component with LightType.Point found! Continuing with current settings...");
            }
        }
        else
        {
            Debug.Log("No Light Component found! Continuing with current settings...");
        }

        //StartCoroutine(FOVRoutine());
    }

   /* private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
        }
    }*/

    private void FixedUpdate()
    {
        FieldOfViewCheck();
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;

                    SoundManager.PlaySound(SoundManager.Sound.DetectingGhost, this.transform.position);

                    //SoundManager.PlaySound(SoundManager.Sound.MetalPipe);

                }
                else
                    canSeePlayer = false;
            }
            else
            {
                canSeePlayer = false;
            }

        }
        else if (canSeePlayer)
            canSeePlayer = false;



        testToggle.isOn = canSeePlayer;
    }
}