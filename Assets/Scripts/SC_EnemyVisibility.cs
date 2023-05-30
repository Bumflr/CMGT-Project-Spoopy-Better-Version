using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_EnemyVisibility : MonoBehaviour
{
    private SkinnedMeshRenderer meshRenderer;
    private ParticleSystem particleSystem;

    private bool beingLit;

    public float delayUntilInvisble = .1f;
    private float timeStamp;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void LateUpdate()
    {
        if (!beingLit)
        {
            //Not being lit anymore
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            particleSystem.Pause();
            particleSystem.Clear();
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

        particleSystem.Play();
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }
}
