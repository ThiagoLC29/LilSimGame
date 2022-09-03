using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType { Item1, Item2, Item3, Item4, Item5, Item6 } //TODO change names from Item1-6 to actual item names

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite() //gets sprites from ItemAssets
    {
        switch (itemType)
        {
            case ItemType.Item1: return ItemAssets.Instance.sprite1;
            case ItemType.Item2: return ItemAssets.Instance.sprite2;
            case ItemType.Item3: return ItemAssets.Instance.sprite3;
            case ItemType.Item4: return ItemAssets.Instance.sprite4;
            case ItemType.Item5: return ItemAssets.Instance.sprite5;
            case ItemType.Item6: return ItemAssets.Instance.sprite6;
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
            case ItemType.Item4:
            case ItemType.Item5:
            case ItemType.Item6:

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
            case ItemType.Item4: return 40;
            case ItemType.Item5: return 50;
            case ItemType.Item6: return 60;
            default: return 0;
        }
    }

}
