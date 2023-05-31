using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerController : MonoBehaviour, IDataPersistence
{
    [Header("Dependencies")]
    public UI_Inventory uiInventory;
    public SC_PlayerWeaponManager playerWeaponScript;
    public SC_PlayerStateLogic playerMovementScript;

    public PlayerInventory playerInventory;

    private void Awake()
    {
        playerInventory = new PlayerInventory(UseItem);
        uiInventory.SetInventory(playerInventory);

        Item flashlight = new Item { itemType = ItemType.Flashlight, amount = 1 };
        playerInventory.AddItem(flashlight);
        playerInventory.EquipItem(flashlight);
        playerWeaponScript.SwitchWeapon(flashlight);

        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    public void SaveData(GameData data)
    {
        data.playerItems = playerInventory.GetItemList();
    }
    public void LoadData(GameData data)
    {
        if (data.playerItems != null && data.playerItems.Count > 0)
        {
            playerInventory.SetItemList(data.playerItems);
        }
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
            case ItemType.Camera:
            case ItemType.Flashlight:
            case ItemType.Lantern:
            case ItemType.FlashGrenade:
                playerWeaponScript.SwitchWeapon(item);
                playerInventory.EquipItem(item);
                break;
            case ItemType.FlashLightBatteries:
                playerWeaponScript.LoadAmmo(item);
                break;
            case ItemType.Coin:
                break;
            default:
                break;
        }
    }
}
