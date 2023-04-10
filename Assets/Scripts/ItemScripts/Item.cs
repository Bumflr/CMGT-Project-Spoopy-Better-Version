using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemAsset;
using static UnityEditor.Progress;

public class Item 
{
    public enum ItemType
    {
        Flashlight,
        Lantern,
        Camera,

        ManaPotion,
        Coin,
        Medkit,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        return ItemAsset.Instance.spriteDictionary[itemType];
    }

    public bool isStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.ManaPotion:
            case ItemType.Coin: return true;
            case ItemType.Camera:
            case ItemType.Flashlight:
            case ItemType.Lantern:
            case ItemType.Medkit: return false;
        }
    }

}
