using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_HidingSpot : MonoBehaviour
{
    public SC_PlayerController currentHidingObject;

    public GameObject obstacleCube;
    private bool amHoveringOverHideyHole;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && amHoveringOverHideyHole)
        {
            obstacleCube.SetActive(true);

            currentHidingObject.transform.position = this.transform.position;
            currentHidingObject.playerMovementScript.isHiding = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl) || !amHoveringOverHideyHole)
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

            currentHidingObject = other.GetComponent<SC_PlayerController>();
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
