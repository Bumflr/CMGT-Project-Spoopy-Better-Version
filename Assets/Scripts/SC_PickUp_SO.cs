using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Item;

[CreateAssetMenu(fileName = "PickUpManagerScriptableObject", menuName = "ScriptableObjects/Pickup Manager")]
public class SC_PickUp_SO : ScriptableObject
{
    [System.NonSerialized] public UnityEvent<string, Sprite, string> pickUpEvent;

    private void OnEnable()
    {
        if (pickUpEvent == null)
            pickUpEvent = new UnityEvent<string, Sprite, string>();

    }

    //Display the item, but also what it is, but also an description, and also how many if that is relevant, but also make it applicable for the logs
    public void PickUpItem(Item item) { SendItemDetails(item); GameStateManager.Instance.SetState(GameState.PickUpItemScreen); }

    private void SendItemDetails(Item item) 
    {
        pickUpEvent.Invoke(item.itemType.ToString(), item.GetSprite(), item.GetDescription());
    }
}
