using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrorRadiusScript : MonoBehaviour
{
    [Header("Dependencies")]
    public SC_CameraManager_SO cameraManager;

    [Header("Settings")]
    public LayerMask whatIsGhost;
    public float radius;

    private GameObject closestGhost;
    private SC_EnemyLogic closestGhostLogic;

    private void Awake()
    {
        //Give a float value from 0 to 1, the cameraManager script can be used to configure how much we want the valeu to change
        cameraManager.ChangeLensSize(1);
    }

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
                cameraManager.ChangeLensSize(1);
            }
        }

    }
    private void CalculateDistance()
    {
        var distance = Vector3.Distance(this.transform.position, closestGhost.transform.position);

        var percentage = distance / radius;

        var value = Mathf.Lerp(.7f, 1f, percentage);

        cameraManager.ChangeLensSize(value);

        cameraManager.ShakeCamera(1 - value);

        //SoundManager.PlaySound(SoundManager.Sound.TerrorRadiusSound, this.transform.position, percentage * 2 );
    }

    float closestDistance(GameObject closestGhost)
    {
        return Vector3.Distance(transform.position, closestGhost.transform.position);
    }
}
