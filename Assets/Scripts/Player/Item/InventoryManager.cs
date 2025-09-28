using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    public ItemSlot[] itemSlot;

    public static HashSet<string> collectedItems = new HashSet<string>();

    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                Debug.Log("itemName = " + itemName + "quantity = " + quantity + "itemSprite = " + itemSprite + "item desc: " + itemDescription);
                itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                return;
            }
        }
    }
/*
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
*/
    public static void MarkItemAsCollected(string itemId)
    {
        collectedItems.Add(itemId);
        Debug.Log($"Marked {itemId} as collected. Total collected: {collectedItems.Count}");
    }

    public static bool IsItemCollected(string itemId)
    {
        return collectedItems.Contains(itemId);
    }
}