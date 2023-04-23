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
   
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public GameObject playerRef;
    //public Toggle testToggle;
    private UsableItem flashlight;

    public bool canSeeEnemy { get; private set; }

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

        flashlight = GetComponent<UsableItem>();
    }
    private void FixedUpdate()
    {
        FieldOfViewCheck();
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0 && flashlight.flashlightToggle)
        {
            for (int i = 0; i < rangeChecks.Length; i++)
            {
                Transform target = rangeChecks[i].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        canSeeEnemy = true;

                        SoundManager.PlaySound(SoundManager.Sound.DetectingGhost, this.transform.position);

                        target.gameObject.GetComponent<SC_EnemyVisibility>().BeingLit();
                        //SoundManager.PlaySound(SoundManager.Sound.MetalPipe);
                    }
                    else
                    {
                        //canSeeEnemy = false;
                        //target.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                    }
                }
                else
                {
                   // canSeeEnemy = false;
                    //target.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                }

            }
        }
        else canSeeEnemy = false;
    }
}