using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;


public class SC_PlayerWeaponManager : MonoBehaviour
{
    private SC_PlayerController characterController;
    public Dictionary<ItemType, GameObject> usableItems = new Dictionary<ItemType, GameObject>();
    public Dictionary<GameObject, ItemType> invertedUsableItems = new Dictionary<GameObject, ItemType>();

    public int NUM_UsableItems => usableItems.Count;


    private UsableItem currentlyEquippedWeapon;

    [System.Serializable]
    public class WeaponThing 
    { 
        public GameObject weapon;
        public ItemType type;
    }

    public WeaponThing[] weaponPrefabs;

    void Awake()
    {
        characterController = GetComponent<SC_PlayerController>();

        foreach (var weapon in weaponPrefabs)
        {
            var newWeapon = Instantiate(weapon.weapon, this.transform);

            newWeapon.GetComponent<UsableItem>().weaponsManager = this;
            newWeapon.SetActive(false);

            usableItems.Add(weapon.type, newWeapon);
            invertedUsableItems.Add(newWeapon, weapon.type);
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

        Debug.Log($"currentlyEquippedWeapon is: {currentlyEquippedWeapon}");
         
       // SetAmountOfAmmo(currentlyEquippedWeapon.gameObject, (int)currentlyEquippedWeapon.maxAmmo);

        currentlyEquippedWeapon.Enter();
    }
    public void LoadAmmo(Item item)
    {
        UsableItem thisItem = null;

        switch (item.itemType)
        {
            case ItemType.FlashLightBatteries:
                thisItem = usableItems[ItemType.Flashlight].GetComponent<UsableItem>();
                thisItem.Ammo += item.amount;
                break;
            default:
                Debug.Log("This item cannot be used as ammo!");
                return;
        }

        characterController.playerInventory.RemoveItem(item);

        if (thisItem.Ammo > thisItem.maxAmmo)
        {
            int sumOfRemainder = (int)thisItem.Ammo - (int)thisItem.maxAmmo;

            thisItem.Ammo = thisItem.maxAmmo;


            Debug.Log(sumOfRemainder);

            Item newItem = new Item { itemType = item.itemType, amount = sumOfRemainder };

            characterController.playerInventory.AddItem(newItem);
            //iT DONT SHOW UP but hey ta least it is there in code
            //Debug.Log(characterController.playerInventory.OnItemListChanged)
        }

        //IF SOME ammo remains, add a new item with the remaining amount. 
        //
    }

    public void SetAmountOfAmmo(GameObject usableItem, int ammo)
    {
        ItemType itemtype = invertedUsableItems[usableItem];

        Item newItem = new Item { itemType = itemtype, amount = ammo };

        characterController.playerInventory.SetItem(newItem);
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
