using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UI_Inventory : MonoBehaviour
{
    private PlayerInventory inventory;

    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private Transform equipItemSlotContainer;

    private GameObject bgObject;
    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");

        equipItemSlotContainer = transform.Find("equipItemSlotContainer");

        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;

        bgObject = transform.Find("BG").gameObject;
    }
    private void OnGameStateChanged(GameState newGameState)
    {
        if (newGameState != GameState.Gameplay)
        {
            this.gameObject.SetActive(true);
            bgObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
            bgObject.SetActive(false);
        }
    }
    public void SetInventory(PlayerInventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach(Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 133 + (1/3);

        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();

            itemSlotRectTransform.GetComponentInChildren<Button>().onClick.AddListener(delegate { inventory.UseItem(item); });

            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(itemSlotTemplate.localPosition.x + (x * itemSlotCellSize), itemSlotTemplate.localPosition.y + (y * itemSlotCellSize));

            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI text = itemSlotRectTransform.Find("AmountText").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                text.SetText(item.amount.ToString());
            }
            else
            {
                text.SetText("");
            }
            x++;
        }

        foreach (Transform child in equipItemSlotContainer)
        {
            Destroy(child.gameObject);
        }

        if (inventory.currentlyequippedItem != null)
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, equipItemSlotContainer).GetComponent<RectTransform>();

            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(0, 0);


            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = inventory.currentlyequippedItem.GetSprite();

            TextMeshProUGUI text = itemSlotRectTransform.Find("AmountText").GetComponent<TextMeshProUGUI>();
            if (inventory.currentlyequippedItem.amount > 1)
            {
                text.SetText(inventory.currentlyequippedItem.amount.ToString());
            }
            else
            {
                text.SetText("");
            }
        }
    }

}
