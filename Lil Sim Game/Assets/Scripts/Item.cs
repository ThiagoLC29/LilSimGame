using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType { Item1, Item2, Item3 } //TODO change names from Item1-3 to actual item names

    public ItemType itemType;
    public int amount;
    public int itemIndex;
    public CustomizableCharacter custom;


    public Sprite GetSprite() //gets sprites from ItemAssets
    {
        switch (itemType)
        {
            case ItemType.Item1: return ItemAssets.Instance.sprite1;
            case ItemType.Item2: return ItemAssets.Instance.sprite2;
            case ItemType.Item3: return ItemAssets.Instance.sprite3;
            default: return null;
        }
    }

    public bool IsStackable() //return true to make items stackable
    {
        switch(itemType)
        {
            case ItemType.Item1:
                return true;
            case ItemType.Item2:
                return true;
            case ItemType.Item3:

            default: return false;
        }
    }

    public int GetPrice() //gets price of each item
    {
        switch (itemType)
        {
            case ItemType.Item1: return 10;
            case ItemType.Item2: return 20;
            case ItemType.Item3: return 30;
            default: return 0;
        }
    }

    public void GetItemIndex()
    {
        switch (itemType)
        {
            case ItemType.Item1:
                itemIndex = 0;
                return;
            case ItemType.Item2:
                itemIndex = 1;
                return;
            case ItemType.Item3:
                itemIndex = 2;
                return;
            default:
                return;
        }
    }
}
