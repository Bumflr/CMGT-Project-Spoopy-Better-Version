using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField] public UI_Inventory uiInventory;
    [SerializeField] private PlayerWeaponScript playerWeaponScript;

    public PlayerMovementScript playerMovementScript;
    public PlayerInventory playerInventory;
    private void Start()
    {
        playerInventory = new PlayerInventory(UseItem);
        uiInventory.SetInventory(playerInventory);

        Item flashlight = new Item { itemType = Item.ItemType.Flashlight, amount = 1 };
        playerInventory.AddItem(flashlight);
        playerInventory.EquipItem(flashlight);
        playerWeaponScript.SwitchWeapon(flashlight);

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
    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }
    private void UseItem(Item item)
    {
        switch (item.itemType) 
        {
            case Item.ItemType.Camera:
            case Item.ItemType.Flashlight:
            case Item.ItemType.Lantern:
            case Item.ItemType.FlashGrenade:
                playerWeaponScript.SwitchWeapon(item);
                playerInventory.EquipItem(item);
                break;
            case Item.ItemType.FlashLightBatteries:
                playerWeaponScript.LoadAmmo(item);
                break;
            default:
            case Item.ItemType.Coin:
                break; 

        }
    }

}
