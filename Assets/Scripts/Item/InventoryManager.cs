using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public GameObject InventoryMenu;

    public ItemSlot[] itemSlot; //UI Slots in the inventory
    public static HashSet<string> CollectedItems => Instance.collectedItems;
    public HashSet<string> collectedItems = new HashSet<string>();
    public static HashSet<string> AlreadyInteractedItems => Instance.alreadyInteractedItems;
	public HashSet<string> alreadyInteractedItems = new HashSet<string>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ApplyAlreadyInteractedItems(SaveData.Instance.AlreadyInteractedItems);
        ApplyCollectedItemsSaveData(SaveData.Instance.CollectedItemIds);
        ApplyInventorySaveData(SaveData.Instance.InventorySlots);
    }

    public static void AddAlreadyInteractedItem(string _item)
    {
        Instance.alreadyInteractedItems.Add(_item);
        if(!SaveData.Instance.AlreadyInteractedItems.Contains(_item))
            SaveData.Instance.AlreadyInteractedItems.Add(_item);
        SaveData.Instance.SavePlayer();
    }

    public bool AddItem(string itemName, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                Debug.Log("itemName = " + itemName + "itemSprite = " + itemSprite + "item desc: " + itemDescription);
                itemSlot[i].AddItem(itemName, itemSprite, itemDescription);

                return true;
            }
        }

        return false;
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
    public List<string> BuildInventorySaveData()
    {
        var list = new List<string>();
        //loops through every UI Slot
        for(int i = 0; i < itemSlot.Length; i++)
        {
            if(itemSlot[i] != null && itemSlot[i].isFull) //only filled slots
            {
                string item = itemSlot[i].itemName;
                list.Add(item);
            }
        }
        return list;
    }

    public List<string> BuildCollectedItemsSaveData()
    {
        return new List<string>(collectedItems);//conver hashset to list
    }

    //Load items
    public void ApplyInventorySaveData(List<string> data)
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
            int itemIndex = MasterList.Instance.EveryCollectableSO
                .FindIndex(item => item.item_name_ == data[i]);

            if(itemIndex < 0) 
            {
                Debug.LogError("Unable to find item in master list");
                continue;
            }

            ItemSO item = MasterList.Instance.EveryCollectableSO[itemIndex];

            AddItem(item.item_name_, item.inv_sprite_, item.desc_);
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

    public void ApplyAlreadyInteractedItems(List<string> ids)
    {
        alreadyInteractedItems.Clear();
        if(ids == null)
        {
            return;
        }
        for(int i = 0; i < ids.Count; i++)
        {
            alreadyInteractedItems.Add(ids[i]);//restore hashset
        }
    }

    public static void MarkItemAsCollected(string itemId)
    {
        Instance.collectedItems.Add(itemId);
        SaveData.Instance.CollectedItemIds.Add(itemId);
        SaveData.Instance.SavePlayer();
        Debug.Log($"Marked {itemId} as collected. Total collected: {Instance.collectedItems.Count}");
    }

    public static bool IsItemCollected(string partialId)
    {
        foreach (string collectedId in CollectedItems)
        {
            if (collectedId.Contains(partialId)) return true;
        }
        return false;
    }
    
    //add item to INv
    public static bool AddItemToInv(InventoryItem _inventoryItem)
    {
        if (InventoryManager.Instance != null)
        {
            return
            InventoryManager.Instance.AddItem(
                _inventoryItem.ItemName,
                _inventoryItem.Sprite,
                _inventoryItem.ItemDescription
            );

        }

        return false;
    }

    public static bool RemoveItemFromInv(string _itemToRemove)
    {
        foreach(ItemSlot slot in InventoryManager.Instance.itemSlot)
        {
            if(slot.itemName == _itemToRemove)
            {
                slot.RemoveItem();
                return true;
            }
        }

        return false;
    }

    public static void DebugPrintAllCollectedItems()
    {
        Debug.Log("collected items:");
        foreach (string item in CollectedItems)
        {
            Debug.Log("Collected Item ID: " + item);
        }
        Debug.Log("end of collected items");
    }
}
