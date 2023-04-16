using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrorRadiusScript : MonoBehaviour
{
    public LayerMask whatIsGhost;
    public float radius;

    private GameObject closestGhost;
    private float closestDistance;
        
    private void Update()
    {
        Collider[] terrorRadiusCollider = Physics.OverlapSphere(this.transform.position, radius, whatIsGhost);

        if (terrorRadiusCollider.Length > 0)
        {
            SoundManager.PlaySound(SoundManager.Sound.TerrorRadiusSound, this.transform.position);
        }

        foreach (Collider ghost in terrorRadiusCollider)
        {
            //float distance = Vector3.Distance(transform.position, ghost.gameObject.transform.position);

            Debug.DrawLine(transform.position, ghost.gameObject.transform.position, Color.yellow);
        }

        //if (Vector3.Distance)
    }

}
