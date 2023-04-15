using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PickupItem : MonoBehaviour
{
    private Item item;
    public ItemType itemType;
    public int amount;

    private void Awake()
    {
        item = new Item { itemType = itemType, amount = this.amount };
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacterController pickingPlayer = other.GetComponent<PlayerCharacterController>();

        if(pickingPlayer != null)
        {
            pickingPlayer.playerInventory.AddItem(item);
            OnPicked(pickingPlayer);
            Destroy(this.gameObject);
        }
    }

    //TODO Make sure a prompt pops up to make sure the player actuall ypicks up the stuff because they might not want to do that.

    private void OnTriggerExit(Collider other)
    {
        PlayerCharacterController pickingPlayer = other.GetComponent<PlayerCharacterController>();

        if (pickingPlayer != null)
        {
        }
    }

    //Do the standard pickup song and dance but depending on what the player picked up
    //You can do other fun wacky shit
    protected virtual void OnPicked(PlayerCharacterController playerController)
    {
        //PlayPickupFeedback();
    }

}
