using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Flashlight,
    Lantern,
    Camera,
    FlashGrenade,

    FlashLightBatteries,
    Coin,
    Medkit,
    AudioLog1,
    AudioLog2,
}

[Serializable]
public class Item
{
    [SerializeField] public ItemType itemType;
    [SerializeField] public int amount;
  
    public Sprite GetSprite()
    {
        return ItemAsset.Instance.spriteDictionary[itemType];
    }
    public string GetDescription()
    {
        return ItemAsset.Instance.descriptionDictionary[itemType];
    }
    public string[] GetText()
    {
        return NoteAsset.Instance.textDictionary[itemType];
    }

    public bool isStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.FlashLightBatteries:
            case ItemType.Coin: return true;
            case ItemType.Camera: return false;
            case ItemType.Flashlight: return false;
            case ItemType.FlashGrenade: return false;
            case ItemType.Lantern: return false;
            case ItemType.Medkit: return false;
        }
    }
    public bool isLog()
    {
        switch (itemType)
        {
            case ItemType.AudioLog1:
                return true;
            case ItemType.AudioLog2:
                return true;
            default:
                return false;
        }
    }


}
