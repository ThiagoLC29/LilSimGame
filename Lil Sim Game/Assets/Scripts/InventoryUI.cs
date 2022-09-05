using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    public CustomizableCharacter custom;
    public Item item;

    private void Awake()
    {
        itemSlotContainer = transform.Find("Item Slot Container");
        itemSlotTemplate = itemSlotContainer.Find("Item Slot Template");
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, EventArgs e)
    {
        RefreshInventoryItems();
    }

    public void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 100f;

        foreach (Item item in inventory.GetItemList())
        {
            this.item = item; //button stuff


            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI uiText = itemSlotRectTransform.Find("AmountText").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                uiText.SetText("x" + item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }

            x++;

            if (x > 6)
            {
                x = 0;
                y--;
            }

        }

    }

    public void EquipSkin() //TODO somehow remake this in the itemslottemplate script
    {
        //TODO: if inside shop area, sell
        item.GetItemIndex();
        custom.skinNr = item.itemIndex;
        item.amount -= 1;


        //if (item.amount > 1)
        //{
        //    item.amount -= 1;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
        RefreshInventoryItems();
    }

}
