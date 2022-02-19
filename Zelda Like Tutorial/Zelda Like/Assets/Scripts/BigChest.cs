using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigChest : ObjectInteraction
{
    public Item contents;
    public bool isOpen;
    public Signal raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    public Inventory playerInventory;
    private Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (!isOpen)
            {
                // Open the chest 
                OpenChest();
            }
            else
            {
                // Chest has already ben opened
                ChestAlreadyOpen();
            }
        }
    }

    public void OpenChest()
    {
        dialogBox.SetActive(true);
        dialogText.text = contents.itemDescription;
        // add contents to inventory
        playerInventory.AddItem(contents); 
        playerInventory.currentItem = contents; // contents of the chest
        raiseItem.Raise();
        isOpen = true;
        context.Raise(); // Remember this is the question mark over your player
        animator.SetBool("opened", true);
    }

    public void ChestAlreadyOpen()
    {
        dialogBox.SetActive(false);
        raiseItem.Raise();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = false;
        }
    }
}
