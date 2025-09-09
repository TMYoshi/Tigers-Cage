using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlot; 
  
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && menuActivated)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false); // deactivates menu screen
            menuActivated = false;
        }

        else if (Input.GetButtonDown("Inventory") && !menuActivated)
        {
            Time.timeScale = 0; // may cause issues with animations when pausing
            InventoryMenu.SetActive(true); // activates menu screen
            menuActivated = true;
        }
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++) {
            if (itemSlot[i].isFull == false)
            {
                Debug.Log("itemName = " + itemName + "quantity = " + quantity + "itemSprite = " + itemSprite + "item desc: " + itemDescription);
                itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                return;
            }
        }

    }

    public void DeselectAllSlots()
    {
        Debug.Log("=== DeselectAllSlots called ===");
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i] != null)
            {
                Debug.Log($"Slot {i}: selectedShader = {(itemSlot[i].selectedShader != null ? itemSlot[i].selectedShader.name : "NULL")}");

                if (itemSlot[i].selectedShader != null)
                {
                    bool wasActive = itemSlot[i].selectedShader.activeSelf;
                    itemSlot[i].selectedShader.SetActive(false);
                    itemSlot[i].thisItemSelected = false;

                    if (wasActive)
                    {
                        Debug.Log($"Deactivated slot {i}'s selectedShader: {itemSlot[i].selectedShader.name}");
                    }
                }
            }
        }
    }
}