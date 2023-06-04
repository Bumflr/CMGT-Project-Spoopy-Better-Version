using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_EnemyVisibility : MonoBehaviour
{
    private SkinnedMeshRenderer meshRenderer;
    private ParticleSystem particleSystem;

    private bool beingLit;
    public bool BeingLit { get { return justVisuals ? false :beingLit; } }
    private bool justVisuals;

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

    public void Lit(bool justVisuals)
    {
        //NBeinf lit af fam
        this.justVisuals = justVisuals;

        beingLit = true;

        timeStamp = Time.time + delayUntilInvisble;

        particleSystem.Play();
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }
}
