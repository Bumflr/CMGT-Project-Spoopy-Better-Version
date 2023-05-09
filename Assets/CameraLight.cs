using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using static UnityEngine.EventSystems.EventTrigger;

public class CameraLight : UsableItem
{
    public Light lighting;


    public float timeFlash = 1f;
    private float cameraTimer;

    public AnimationCurve lightingCurve;
    public AnimationCurve lightingIntensityCurve;

    private GameObject savedParent;

    private void Awake()
    {
        Ammo = maxAmmo;

        savedParent = this.transform.parent.gameObject;
    }

    public override void Enter()
    {
        base.Enter();

        this.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        flashlightToggle = false;

        this.gameObject.SetActive(false);
    }

    public override void PhsyicsUpdate()
    {
        if (tapInput && !flashlightToggle)
        {
            UsedTap();

            Ammo -= 1f;

            cameraTimer = timeFlash;

            flashlightToggle = true;

            lighting.enabled = true;


            this.transform.parent = null;
        }

        if (cameraTimer <= 0f)
        {
            flashlightToggle = false;
        }

        if (flashlightToggle && Ammo >= 0)
        {
            cameraTimer -= 0.01f;

            lighting.enabled = true;
        }
        else
        {
            lighting.enabled = false;

            this.transform.parent = savedParent.transform;

            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        CurrentLightingLevel();
    }

    void CurrentLightingLevel()
    {
        float percentage = 1 - (cameraTimer / timeFlash);

        //Goes from 1.0 to 0.0

        lighting.range = lightingCurve.Evaluate(percentage);
        lighting.intensity = lightingIntensityCurve.Evaluate(percentage);
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            Debug.Log("Gotcha");
        }
    }
}
