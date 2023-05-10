using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItem : MonoBehaviour
{
    public PlayerWeaponScript weaponsManager;

    protected bool tapInput;
    protected bool holdInput;

    public bool flashlightToggle = false;

    private float ammo;
    public float Ammo { get {  return ammo; } 
        set
        {
            //When the value of ammo changes, notify weaponsmanager so It can set the value of the ammo properly
            //I do not want to do a for loop to check every time I look for this value, so I only do this when it SETS it
            if (ammo != value)
            {
                ammo = value;
                if (weaponsManager != null)
                    weaponsManager.SetAmountOfAmmo(this.gameObject, (int)ammo);
            }
        }
    }

    public float maxAmmo = 3;

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
