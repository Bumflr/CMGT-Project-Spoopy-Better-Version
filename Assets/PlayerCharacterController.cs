using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private PlayerWeaponScript playerWeaponScript;

    public PlayerInventory playerInventory;
    private void Start()
    {
        playerInventory = new PlayerInventory(UseItem);
        uiInventory.SetInventory(playerInventory);

        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
    private void OnGameStateChanged(GameState newGameState)
    {
        if (newGameState != GameState.Gameplay)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }

    private void UseItem(Item item)
    {
        switch (item.itemType) 
        {
            case Item.ItemType.Flashlight:
            case Item.ItemType.Lantern:
                playerWeaponScript.SwitchWeapon(item);
                playerInventory.EquipItem(item);
                break;
            default:
            case Item.ItemType.Coin:
                break; 

        }
    }

}
