using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Toggles")]
    public bool canMove = true;
    public bool isAnythingOpen = false;
    public bool isInventoyOpen = false;
    public bool isShopOpen = false;
    public bool inShopRange = false;

    [Header("Movement")]
    public float moveSpeed;
    public Rigidbody2D rb;

    private Vector2 moveDirection;

    [Header("UI Related")]
    public GameObject inventoryPanel;
    public GameObject shop;
    private Inventory inventory;
    [SerializeField] private InventoryUI inventoryUI;


    private void Awake()
    {
        inventory = new Inventory();
        
    }
    void Update()
    {
        GetInputs();
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

        moveDirection = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.I) && isShopOpen == false)
            OpenInventory();

        if (inShopRange && Input.GetKeyDown(KeyCode.E))
        {
            OpenInventory();
            TalkToShopkeeper();
        }
    }

    void Move() //Apply movement
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            Debug.Log("Press E to talk to shopkeeper"); //TODO call another method to actually talk to shopkeeper
            inShopRange = true;
        }

        if (collision.tag == "Item")
        {
            ItemWorld itemWorld = collision.GetComponent<ItemWorld>();

            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
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
        shop.SetActive(!isShopOpen);
        isShopOpen = !isShopOpen;

        if (isShopOpen)
            canMove = false;
        else
            canMove = true;

    }
}
