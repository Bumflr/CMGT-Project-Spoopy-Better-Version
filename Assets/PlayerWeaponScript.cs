using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponScript : MonoBehaviour
{
    public ArsenalObject[] arsenalObjects;
    public int NUM_ARSENALOBJECTS => arsenalObjects.Length;

    private ArsenalObject activeArsenalObject;

    void Awake()
    {

        foreach (ArsenalObject arsenalObject in arsenalObjects)
        {
            arsenalObject.gameObject.SetActive(false);
        }

        SwitchArsenal(0);
    }


    public void SwitchArsenal(int controlSchemeIndex)
    {
        if (activeArsenalObject != null) activeArsenalObject.Exit();

        if (controlSchemeIndex > NUM_ARSENALOBJECTS - 1 || controlSchemeIndex < -1)
        {
            Debug.LogError("Supplied control scheme index is not a valid number");
            return;
        }

        activeArsenalObject = arsenalObjects[controlSchemeIndex];
        Debug.Log($"activeControlScheme is: {activeArsenalObject}");

        activeArsenalObject.Enter();
    }
    void Update()
    {
        activeArsenalObject.LogicUpdate();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchArsenal(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchArsenal(1);
        }
    }

    void FixedUpdate()
    {
        activeArsenalObject.PhsyicsUpdate();
    }
}
