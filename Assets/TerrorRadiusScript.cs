using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrorRadiusScript : MonoBehaviour
{
    public LayerMask whatIsGhost;
    public float radius;
    public CinemachineVirtualCamera vmCam;

    private GameObject closestGhost;

        
    private void Update()
    {
        Collider[] terrorRadiusCollider = Physics.OverlapSphere(this.transform.position, radius, whatIsGhost);

        foreach (Collider ghost in terrorRadiusCollider)
        {
            float distance = Vector3.Distance(transform.position, ghost.gameObject.transform.position);

            Debug.DrawLine(transform.position, ghost.gameObject.transform.position, Color.yellow);

            //If the distance of this thing is less than the distance from the closestGhost, this becomes the closestGhost

            if (closestGhost == null)
            {
                closestGhost = ghost.gameObject;
            }

            if (distance < closestDistance(closestGhost))
            {
                closestGhost = ghost.gameObject;
            }
        }

        if (closestGhost != null)
        {
            Debug.DrawLine(transform.position, closestGhost.gameObject.transform.position, Color.magenta);

            if (closestDistance(closestGhost) >= radius)
            {
                //it must have left the radius and it didnt find something new so reset it
                closestGhost = null;
            }
            else if (terrorRadiusCollider.Length > 0)
            {
                CalculateDistance();
            }
            else
            {
                vmCam.m_Lens.OrthographicSize = 6;
            }
        }

    }
    private void CalculateDistance()
    {
        var distance = Vector3.Distance(this.transform.position, closestGhost.transform.position);

        var percentage = distance / radius;

        var value = Mathf.Lerp(4.2f, 6.0f, percentage);

        vmCam.m_Lens.OrthographicSize = value;

        SoundManager.PlaySound(SoundManager.Sound.TerrorRadiusSound, this.transform.position, percentage * 2 );
    }

    float closestDistance(GameObject closestGhost)
    {
        return Vector3.Distance(transform.position, closestGhost.transform.position);
    }
}
