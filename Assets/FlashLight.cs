using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLight : ArsenalObject
{
    public Light lighting;
    public Text testtext;

    public float maxEnergy = 10;
    public float energy = 10;

    private bool flashlightToggle;
    private MeshCollider mesh;

    private void Awake()
    {
        mesh = GetComponent<MeshCollider>();
    }

    public override void Enter()
    {
        base.Enter();

        this.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

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



        if (flashlightToggle && energy >= 0)
        {
            energy -= 0.01f;

            testtext.text = energy.ToString();

            lighting.enabled = true;
        }
        else
        {
            lighting.enabled = false;
        }
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            Debug.Log("Gotcha");
        }
    }

}
