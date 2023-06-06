using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NotesManagerSCriptableObject", menuName = "ScriptableObjects/Notes Manager")]
public class SC_Notes_SO : ScriptableObject
{
    [System.NonSerialized] public UnityEvent<string, Sprite, string[]> startReadEvent;

    private void OnEnable()
    {
        if (startReadEvent == null)
            startReadEvent = new UnityEvent<string, Sprite, string[]>();
    }

    //Display the item, but also what it is, but also an description, and also how many if that is relevant, but also make it applicable for the logs
    public void StartReadingItem(Item item) { SendItemDetails(item); GameStateManager.Instance.SetState(GameState.ReadScreen); }

    private void SendItemDetails(Item item)
    {

        startReadEvent.Invoke(item.itemType.ToString(), item.GetSprite(), item.GetText());
    }
       

}
