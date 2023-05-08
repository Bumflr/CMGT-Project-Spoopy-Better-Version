using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItem : MonoBehaviour
{

    protected bool tapInput;
    protected bool holdInput;

    public bool flashlightToggle;

    public float ammo;
    public float maxAmmo = 10;

    public virtual void LogicUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tapInput = true;

            holdInput = true;
            Debug.Log("Input received!");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            holdInput = false;
        }
    }
    public virtual void PhsyicsUpdate() { }

    public virtual void Enter() { }
    public virtual void Exit() { }

    public virtual void UsedTap() { tapInput = false; }
}
