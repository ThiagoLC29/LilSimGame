using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IShopCustomer
{
    [Header("Toggles")]
    public bool canMove = true;
    public bool isAnythingOpen = false;
    public bool isInventoyOpen = false;
    public bool isShopOpen = false;
    public bool inShopRange = false;
    public bool isWalking = false;

    [Header("Movement")]
    public float moveSpeed;
    public Rigidbody2D rb;

    private Vector2 moveDirection;

    [Header("UI Related")]
    public GameObject inventoryPanel;
    public GameObject shop;
    public GameObject shopDialogue;
    private Inventory inventory;
    [SerializeField] private InventoryUI inventoryUI;
    public Dialogue dialogue;

    [Header("Other Things")]
    public int money;
    public ShopUI shopUI;
    public IShopCustomer shopCustomer;
    public Animator animator;

    private void Awake()
    {
        inventory = new Inventory();
        money = 50; //just for debugging
        shopUI = FindObjectOfType<ShopUI>().GetComponent<ShopUI>();
        
    }
    void Update()
    {
        GetInputs();
        animator.SetBool("isWalking", isWalking);
    }

    private void FixedUpdate()
    {
        if (canMove)
            Move();
    }

    void GetInputs() //Get inputs from player
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX > 0) //flips player right
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (moveX < 0) //flips player left
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        moveDirection = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.I) && isShopOpen == false)
            OpenInventory();

        if (inShopRange && Input.GetKeyDown(KeyCode.E))
        {
            OpenInventory();
            TalkToShopkeeper();
        }

        if (Input.GetKeyDown(KeyCode.M)) //just for debugging
            money += 50;
    }

    void Move() //Apply movement
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);

        

        if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
        {
            isWalking = true;
        }
        else
            isWalking = false;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3) //Layer 3: Interactable
        {
            Debug.Log("Press E to interact"); //TODO: actually show this message onscreen


            if (collision.tag == "Shopkeeper")
            {
                inShopRange = true;
            }

        }

        if (collision.gameObject.layer == 6) //Layer 6: Item
        {
            ItemWorld itemWorld = collision.GetComponent<ItemWorld>();

            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3) //Layer 3: Interactable
        {
            Debug.Log("Too far to talk to shopkeeper"); //TODO disable UI "press E"
            inShopRange = false;
        }
    }

    void OpenInventory()
    {
        inventoryPanel.SetActive(!isInventoyOpen);
        isInventoyOpen = !isInventoyOpen;

        inventoryUI.SetInventory(inventory); //trying setting up here instead of Awake()

    }

    void TalkToShopkeeper()
    {
        if (dialogue.alreadyTalked == false)
        {
            dialogue = shopDialogue.GetComponent<Dialogue>();
            shopDialogue.SetActive(true);
        }
        

        if (dialogue.inDialogue == false)
        {
            shop.SetActive(!isShopOpen);
            isShopOpen = !isShopOpen;

            if (isShopOpen)
            {
                canMove = false;
                shopUI.Show(shopCustomer);

            }
            else
            {
                shopUI.Hide();
                dialogue.alreadyTalked = false;
                canMove = true;
            }
        }

    }

    public void BoughtItem(Item.ItemType itemType)
    {
        Debug.Log("Bought Item: " + itemType);
    }
}
