using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_EnemyVisibility : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private bool beingLit;

    public float delayUntilInvisble = .1f;
    private float timeStamp;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void LateUpdate()
    {
        if (!beingLit)
        {
            //Not being lit anymore
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }

        if (timeStamp <= Time.time)
        {
            beingLit = false;
        }
    }

    public void BeingLit()
    {
        //NBeinf lit af fam

        beingLit = true;
        timeStamp = Time.time + delayUntilInvisble;

        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }
}
