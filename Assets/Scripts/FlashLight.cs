using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLight : UsableItem
{
    public Light lighting;


    public AnimationCurve lightingCurve;
    public AnimationCurve lightingIntensityCurve;

    private void Awake()
    {
        ammo = maxAmmo;
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

    //NOTE: Constantly Depleting Flashlight

    public override void PhsyicsUpdate()
    {
        if (tapInput)
        {
            UsedTap();

            flashlightToggle = !flashlightToggle;
        }

        if (flashlightToggle && ammo >= 0)
        {
            ammo -= 0.01f;

            //testtext.text = energy.ToString();

            lighting.enabled = true;
        }
        else
        {
            lighting.enabled = false;
        }

        CurrentLightingLevel();
    }

    void CurrentLightingLevel()
    {
        float percentage = 1 - (ammo / maxAmmo);

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
