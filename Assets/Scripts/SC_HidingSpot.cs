using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_HidingSpot : MonoBehaviour
{
    public PlayerCharacterController currentHidingObject;

    public GameObject obstacleCube;
    private bool amHoveringOverHideyHole;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && amHoveringOverHideyHole)
        {
            obstacleCube.SetActive(true);

            currentHidingObject.transform.position = this.transform.position;
            currentHidingObject.playerMovementScript.isHiding = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || !amHoveringOverHideyHole)
        {
            obstacleCube.SetActive(true);
            if (currentHidingObject != null)
            currentHidingObject.playerMovementScript.isHiding = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            amHoveringOverHideyHole = true;

            currentHidingObject = other.GetComponent<PlayerCharacterController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            amHoveringOverHideyHole = false;
        }
    }
}
