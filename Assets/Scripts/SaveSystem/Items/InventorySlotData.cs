using UnityEngine;
using System;

[Serializable]
public class InventorySlotData
{
    //unique identifier for the item
    public string itemId;
    //How many of the item
    public int quantity;
    //Path used by Resources.Load()
    public string spriteResourcePath;
    //Text descritpion show in the UI
    public string itemDescription;

    //Constructor used when saving inventory
    public InventorySlotData(string itemId, int quantity, string spriteResourcePath, string itemDescription)
    {
        this.itemId = itemId;
        this.quantity = quantity;
        this.spriteResourcePath = spriteResourcePath;
        this.itemDescription = itemDescription;
    }
    
}
