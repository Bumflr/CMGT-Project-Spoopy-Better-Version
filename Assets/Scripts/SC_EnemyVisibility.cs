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
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }

        if (timeStamp <= Time.time)
        {
            beingLit = false;
        }
    }

    public void BeingLit()
    {
        beingLit = true;
        timeStamp = Time.time + delayUntilInvisble;

        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }
}
