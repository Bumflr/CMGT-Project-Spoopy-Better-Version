using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory 
{
    public event EventHandler OnItemListChanged;
    private List<Item> itemList;
    private Action<Item> useItemAction;
    public Item currentlyequippedItem { get; private set; }

    public PlayerInventory(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;

        itemList = new List<Item>();

        Debug.Log(itemList.Count);
    }

    public void AddItem(Item item)
    {
        if (item.isStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if (item.itemType == inventoryItem.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory) { itemList.Add(item); }
        }
        else
        {
            itemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public void RemoveItem(Item item)
    {
        if (item.isStackable())
        {
            Item itemInInventory = null;

            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0) 
            { 
                itemList.Remove(itemInInventory);
            }
        }
        else
        {
            itemList.Remove(item);
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public void UseItem(Item item) {  useItemAction(item); }
    public void EquipItem(Item item)  { currentlyequippedItem = item; OnItemListChanged?.Invoke(this, EventArgs.Empty); }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
