using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_TeleportPlayer : MonoBehaviour
{
    [SerializeField] private Transform destination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<SC_PlayerMovement>(out var playerMovement))
        {
            playerMovement.Teleport(destination.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(destination.position, .4f);
        var direction = destination.TransformDirection((Vector3.forward));
        Gizmos.DrawRay(destination.position, direction);
    }
    
    
}
