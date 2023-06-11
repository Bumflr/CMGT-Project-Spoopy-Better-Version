using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLightFOVCheck : MonoBehaviour
{
    [Header("Settings")]
    public float radius;
    [Range(0, 360)]
    public float angle;
   
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public GameObject playerRef;
    public bool justVisuals;

    //public Toggle testToggle;
    private UsableItem flashlight;

    public Light lighting;
    public bool canSeeEnemy { get; private set; }

    private Collider[] rangeChecks;
    private void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Ghost");

        if (TryGetComponent<Light>(out Light lighting))
        {
            this.lighting = lighting;

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

    private void Update()
    {
        FieldOfViewCheck();

        if (lighting != null)
        {
            radius = lighting.range;
        }
    }

    private void FieldOfViewCheck()
    {
        //Collider[] rangeChecks;
        rangeChecks = new Collider[10];


        int numColliders = Physics.OverlapSphereNonAlloc(transform.parent != null ? transform.parent.position : transform.position, radius, rangeChecks, targetMask);

        if (numColliders > 0 && flashlight != null ? flashlight.flashlightToggle : true)
        {
            for (int i = 0; i < numColliders; i++)
            {
                Transform target = rangeChecks[i].transform;
                Vector3 directionToTarget = (target.position - (transform.parent != null ? transform.parent.position : transform.position)).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.parent != null ? transform.parent.position : transform.position, target.position);

                    if (!Physics.Raycast(transform.parent != null ? transform.parent.position : transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        canSeeEnemy = true;

                       // SoundManager.PlaySound(SoundManager.Sound.DetectingGhost, this.transform.position);

                        target.gameObject.GetComponent<SC_EnemyVisibility>().Lit(justVisuals);
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