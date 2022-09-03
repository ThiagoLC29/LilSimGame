using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.prefabItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    private Item item;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMeshPro;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("ItemWorldText").GetComponent<TextMeshPro>();
    }


    public void SetItem(Item item) //changes item sprite and shows amount
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();

        if (item.amount > 1)
        {
            textMeshPro.SetText("x" + item.amount.ToString());
        }
        else
        {
            textMeshPro.SetText("");
        }
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf() //this is basically a spawnpoint, so it should destroy itself
    {
        Destroy(gameObject);
    }

}
