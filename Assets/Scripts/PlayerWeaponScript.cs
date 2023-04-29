using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using static Item;

public class PlayerWeaponScript : MonoBehaviour
{
    private PlayerCharacterController characterController;
    public Dictionary<Item.ItemType, GameObject> usableItems = new Dictionary<Item.ItemType, GameObject>();
    public int NUM_UsableItems => usableItems.Count;

    private UsableItem currentlyEquippedWeapon;

    [System.Serializable]
    public class WeaponThing 
    { 
        public GameObject weapon;
        public Item.ItemType type;
    }

    public WeaponThing[] weaponPrefabs;

    void Awake()
    {
        characterController = GetComponent<PlayerCharacterController>();

        foreach (var weapon in weaponPrefabs)
        {
            var newWeapon = Instantiate(weapon.weapon, this.transform);

            newWeapon.SetActive(false);

            usableItems.Add(weapon.type, newWeapon);
        }

    }

    public void SwitchWeapon(Item item)
    {
        if (currentlyEquippedWeapon != null)
        {
            currentlyEquippedWeapon.Exit();
            currentlyEquippedWeapon.gameObject.SetActive(false);
        }

        currentlyEquippedWeapon = usableItems[item.itemType].GetComponent<UsableItem>();

        currentlyEquippedWeapon.gameObject.SetActive(true);
        Debug.Log($"activeControlScheme is: {currentlyEquippedWeapon}");

        currentlyEquippedWeapon.Enter();
    }
    public void LoadAmmo(Item item)
    {
        UsableItem thisItem = null;

        switch (item.itemType)
        {
            case Item.ItemType.FlashLightBatteries:
                thisItem = usableItems[Item.ItemType.Flashlight].GetComponent<UsableItem>();
                thisItem.ammo += item.amount;
                break;
            default:
                Debug.Log("This item cannot be used as ammo!");
                return;
        }

        if (thisItem.ammo > thisItem.maxAmmo)
        {
            int sumOfRemainder = (int)thisItem.ammo - (int)thisItem.maxAmmo;

            thisItem.ammo = thisItem.maxAmmo;


            Debug.Log(sumOfRemainder);

            Item newItem = new Item { itemType = item.itemType, amount = sumOfRemainder };

            characterController.playerInventory.RemoveItem(item);
            characterController.playerInventory.AddItem(newItem);
            //iT DONT SHOW UP but hey ta least it is there in code
            //Debug.Log(characterController.playerInventory.OnItemListChanged)
        }

        //IF SOME ammo remains, add a new item with the remaining amount. 
        //
    }

    void Update()
    {
        if (currentlyEquippedWeapon != null) currentlyEquippedWeapon.LogicUpdate();
    }

    void FixedUpdate()
    {
        if (currentlyEquippedWeapon != null) currentlyEquippedWeapon.PhsyicsUpdate();
    }
}
