using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PickupItem : MonoBehaviour
{
    [Header("Dependencies")]
    public SC_PickUp_SO pickUpManager;

    [Header("Settings")]
    public ItemType itemType;
    public int amount;

    private Item item;
    private SC_PlayerController pickingPlayer;
    private void Awake()
    {
        item = new Item { itemType = itemType, amount = this.amount };
    }

    private void OnTriggerEnter(Collider other)
    {
        pickingPlayer = other.GetComponent<SC_PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && pickingPlayer != null)
        {
            pickingPlayer.playerInventory.AddItem(item);

            pickUpManager.PickUpItem(item);
            GameStateManager.Instance.SetState(GameState.PickUpItemScreen);

            OnPicked(pickingPlayer);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        pickingPlayer = null;
    }

    //TODO Make sure a prompt pops up to make sure the player actuall ypicks up the stuff because they might not want to do that.

    //Do the standard pickup song and dance but depending on what the player picked up
    //You can do other fun wacky shit
    private void OnPicked(SC_PlayerController playerController)
    {
        for (int i = 0; i < playerController.playerWeaponScript.weaponPrefabs.Length; i++)
        {
            if (itemType == playerController.playerWeaponScript.weaponPrefabs[i].type)
            {
                //It found a type match, this item is a weapon, thus equip it.
                playerController.playerInventory.EquipItem(item);
                playerController.playerWeaponScript.SwitchWeapon(item);
            }
        }

        //PlayPickupFeedback();
    }

}
