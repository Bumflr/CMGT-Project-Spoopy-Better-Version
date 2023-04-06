using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArsenalObject : MonoBehaviour
{
    protected bool tapInput;
    protected bool holdInput;

    public virtual void LogicUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tapInput = true;

            holdInput = true;
            Debug.Log("Input received!");
        }

        if (Input.GetMouseButtonUp(0))
        {
            holdInput = false;
        }
    }
    public virtual void PhsyicsUpdate() { }

    public virtual void Enter() { }
    public virtual void Exit() { }

    public virtual void UsedTap() { tapInput = false; }
}
