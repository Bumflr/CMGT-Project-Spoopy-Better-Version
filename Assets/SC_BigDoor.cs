using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BigDoor : MonoBehaviour
{
    public bool isTouchingDoor;
    public void BlowUpDoor(int amount)
    {
        if (amount >= 3 && isTouchingDoor)
        {
            Debug.Log("Kablooey!");
        }
        else
        {
            Debug.Log("Lol you stupid bastard");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            isTouchingDoor = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            isTouchingDoor = false;
        }
    }
}
