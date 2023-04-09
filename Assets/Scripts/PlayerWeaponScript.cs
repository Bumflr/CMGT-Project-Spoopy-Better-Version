using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponScript : MonoBehaviour
{
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

    void Update()
    {
        if (currentlyEquippedWeapon != null) currentlyEquippedWeapon.LogicUpdate();
    }

    void FixedUpdate()
    {
        if (currentlyEquippedWeapon != null) currentlyEquippedWeapon.PhsyicsUpdate();
    }
}
