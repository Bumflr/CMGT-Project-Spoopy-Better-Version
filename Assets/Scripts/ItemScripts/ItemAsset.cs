using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemAsset : MonoBehaviour
{
    private static ItemAsset _Instance;

    public static ItemAsset Instance
    {
        get { if (_Instance == null) _Instance = Instantiate(Resources.Load<ItemAsset>("ItemSpriteDatabase")); return _Instance; }
    }

    [System.Serializable]
    public class SpriteItem
    {
        public Sprite sprite; 
        public Item.ItemType itemType;
    }

    public SpriteItem[] spriteItems;
    private void Awake()
    {
        foreach (var spriteItem in spriteItems)
        {
            spriteDictionary.Add(spriteItem.itemType, spriteItem.sprite);
        }
    }

    public Dictionary<Item.ItemType, Sprite> spriteDictionary = new Dictionary<Item.ItemType, Sprite>();


}
