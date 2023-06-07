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
    }

    public bool AddItem(Item item)
    {
        if (item.isLog())
            return false;
        bool itemAlreadyInInventory = false;

        if (item.isStackable())
        {
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
            foreach (Item inventoryItem in itemList)
            {
                if (item.itemType == inventoryItem.itemType)
                {
                    itemAlreadyInInventory = true;
                    Debug.Log("Item already in inventory and not stackable so get out");
                }
            }
            if (!itemAlreadyInInventory) { itemList.Add(item); }

        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);

        return itemAlreadyInInventory;
    }
    public void RemoveItem(Item item)
    {
        if (item.isLog())
            return;

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


    public void SetItem(Item item)
    {
        Item itemInInventory = null;

        foreach (Item inventoryItem in itemList)
        {
            if (inventoryItem.itemType == item.itemType)
            {
                //This is the item
                itemInInventory = inventoryItem;
            }
        }
        if (itemInInventory != null)
        {
            itemInInventory.amount = item.amount;

            OnItemListChanged?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.LogWarning($"Item: {item} could not be found! ");
        }
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public void SetItemList(List<Item> newItemList)
    {
        this.itemList = newItemList;

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

}
