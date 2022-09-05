using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGambiarra : MonoBehaviour
{
    public InventoryUI inventoryUI;
    public CustomizableCharacter custom;


    void Start()
    {
        inventoryUI = FindObjectOfType<InventoryUI>().GetComponent<InventoryUI>();
        inventoryUI.item.GetItemIndex();
        custom = FindObjectOfType<CustomizableCharacter>().GetComponent<CustomizableCharacter>();
        inventoryUI.item.GetItemIndex();
    }

    void Update()
    {
        
    }

    public void Gambiarra() //brazillian slang for fixing things in a weird way
    {
        custom.skinNr = inventoryUI.item.itemIndex;
        inventoryUI.item.amount -= 1;
        inventoryUI.RefreshInventoryItems();

    }
}
