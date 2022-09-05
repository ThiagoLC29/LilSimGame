using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    private Transform container;
    private Transform shopItemTemplate;
    public Item item, item2, item3;
    private IShopCustomer shopCustomer;
    public Item.ItemType type;


    private void Awake()
    {
        container = transform.Find("Container");
        shopItemTemplate = container.Find("ShopItemTemplate");
    }

    private void Start()
    {
        CreateItemButton(Item.ItemType.Item1, item.GetSprite(), "Blue Outfit", item.GetPrice(), 0);
        CreateItemButton(Item.ItemType.Item2, item2.GetSprite(), "Red Outfit", item2.GetPrice(), 1);
        CreateItemButton(Item.ItemType.Item3, item3.GetSprite(), "Purple Outfit", item3.GetPrice(), 2);

        Hide();
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void CreateItemButton (Item.ItemType itemType, Sprite itemSprite, string itemName, int itemPrice, int positionIndex)
    {
        type = itemType;
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 120f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemTransform.Find("Item Name").GetComponent<TextMeshProUGUI>().SetText(itemName.ToString());
        shopItemTransform.Find("Item Price").GetComponent<TextMeshProUGUI>().SetText("$ " + itemPrice.ToString());
        shopItemTransform.Find("Item Icon").GetComponent<Image>().sprite = itemSprite;

        shopItemTransform.GetComponent<Button>().onClick.AddListener(() => { TryBuyItem(itemType);});
    }
    
    public void TryBuyItem(Item.ItemType itemType)
    {
        Debug.Log("FINALLY" + itemType);
        //shopCustomer.BoughtItem(itemType); //TODO: fix this line and finish the shop
    }
    public void Show(IShopCustomer shopCustomer)
    {
        this.shopCustomer = shopCustomer;
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }


}
