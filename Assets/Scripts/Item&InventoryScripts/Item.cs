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
    FlareGun,

    FlashLightBatteries,
    GasCanister,
    Medkit,
    AudioLog1,
    AudioLog2,
    AudioLog3,
    AudioLog4,
    AudioLog5, 
    AudioLog6,
    AudioLog7,
    Letter1,
    Letter2,
    Letter3,
    Letter4,
    Letter5,
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
            case ItemType.GasCanister: return true;
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
            case ItemType.AudioLog3:
                return true;
            case ItemType.AudioLog4:
                return true;
            case ItemType.AudioLog5:
                return true;
            case ItemType.AudioLog6:
                return true;
            case ItemType.AudioLog7:
                return true;
            case ItemType.Letter1:
                return true;
            case ItemType.Letter2:
                return true;
            case ItemType.Letter3:
                return true;
            case ItemType.Letter4:
                return true;
            case ItemType.Letter5:
                return true;

            default:
                return false;
        }
    }


}
