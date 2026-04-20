using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;

    public ItemSlot[] itemSlot; //UI Slots in the inventory
    public static HashSet<string> collectedItems = new HashSet<string>();

	public static HashSet<string> alreadyInteratedItems = new HashSet<string>();

    public const string SPRITE_RESOURCES_FOLDER = "Item/"; //Folfer inside Assets/Resources/. Used to rebuild sprites when loading
    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                Debug.Log("itemName = " + itemName + "quantity = " + quantity + "itemSprite = " + itemSprite + "item desc: " + itemDescription);
                itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                //add to save system
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

//=======Saveformanager==========
    public List<InventorySlotData> BuildInventorySaveData()
    {
        var list = new List<InventorySlotData>();
        //loops through every UI Slot
        for(int i = 0; i < itemSlot.Length; i++)
        {
            if(itemSlot[i] != null && itemSlot[i].isFull) //only filled slots
            {
                string itemId = itemSlot[i].itemName;
                int  qty = itemSlot[i].quantity;
                string desc = itemSlot[i].itemDescription;

                //save the sprite refrence
                string spritePath = "";
                if(itemSlot[i].itemSprite != null)
                {
                    spritePath = SPRITE_RESOURCES_FOLDER + itemId;

                }

                list.Add(new InventorySlotData(itemId,qty,spritePath,desc));
            }
        }
        return list;
    }

    public List<string> BuildCollectedItemsSaveData()
    {
        return new List<string>(collectedItems);//conver hashset to list
    }

    //Load items
    public void  ApplyInventorySaveData(List<InventorySlotData> data)
    {
        //clear current UI slots
        for(int i = 0; i < itemSlot.Length; i++)
        {
            if(itemSlot[i] != null && itemSlot[i].isFull)
            {
                itemSlot[i].RemoveItem();
            }
        }

        if(data == null)
        {
            return;
        }

        //refill UI
        for(int i = 0; i < data.Count; i++)
        {
            var d = data[i];
            string path = SPRITE_RESOURCES_FOLDER + d.itemId;
            Debug.Log($"Loading sprite for itemId '{d.itemId}' from path '{path}'");

            Sprite sprite = Resources.Load<Sprite>(SPRITE_RESOURCES_FOLDER + d.itemId);
            //load sprite from resources folder
            if (sprite == null)
            {
                Debug.LogWarning($"Sprite for itemId '{d.itemId}' not found at path '{SPRITE_RESOURCES_FOLDER + d.itemId}'. Check if the sprite exists and the path is correct.");
            }

            else
            {
                Debug.Log($"Successfully loaded sprite for itemId '{d.itemId}' from path '{path}'");
            }

            AddItem(d.itemId, d.quantity,sprite, d.itemDescription);
        }
    }

    public void ApplyCollectedItemsSaveData(List<string> ids)
    {
        collectedItems.Clear();
        if(ids == null)
        {
            return;
        }
        for(int i = 0; i < ids.Count; i++)
        {
            collectedItems.Add(ids[i]);//restore hashset
        }
    }
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
