using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BigDoor : MonoBehaviour
{
    [Header("Dependencies")]
    public SC_PlayerController playerController;

    public GameObject bigDoor;
    public GameObject explosion;

    public bool isTouchingDoor =  true;

    private void Awake()
    {
        bigDoor = GameObject.FindGameObjectWithTag("Door");
    }

    public void BlowUpDoor(int amountOfDynamite)
    {
        if (amountOfDynamite >= 2 && isTouchingDoor)
        {
            Debug.Log("Kablooey!");

            GameStateManager.Instance.SetState(GameState.Gameplay);

            Item item = new Item { itemType = ItemType.DynamiteStaff, amount = amountOfDynamite };

            playerController.playerInventory.RemoveItem(item);

            bigDoor.SetActive(false);
            explosion.SetActive(true);
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
