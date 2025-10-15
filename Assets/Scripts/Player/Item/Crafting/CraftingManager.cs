using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

public class CraftingManager : MonoBehaviour
{
    public static InventoryManager _inventory;
    [SerializeField] private InventoryManager _inspectorInventory;

    [SerializeReference]
    public ItemCombinations[] StaticItemCombo;
    public static HashSet<ItemCombinations> CraftingSet = new HashSet<ItemCombinations>();
    public static void CraftIfComboExist(string item1, string item2)
    {
        foreach (ItemCombinations item in CraftingSet)
        {
            if(item == null)
            {
                Debug.Log("No item combo provided");
                continue;
            }
            if(!item.CanCraft(item1, item2)) continue;
            switch (item)
            {
                case ItemComCreation creation:
                    for (int i = 0; i < _inventory.itemSlot.Length; i++)
                    {
                        if (_inventory.itemSlot[i].isFull && 
                        (_inventory.itemSlot[i].itemName == item1 || _inventory.itemSlot[i].itemName == item2))
                        {
                            _inventory.itemSlot[i].RemoveItem();
                        }
                    }
                    _inventory.AddItem(creation.ResultName, 1, creation.ResultImage, creation.ResultDescription);
                    break;
                case ItemComDialog dialog:
                    throw new System.NotImplementedException();
                default:
                    // Optional: handle other types
                    break;
            }
        }
    }

    public void AddToHash(ItemCombinations item)
    {
        CraftingSet.Add(item);
    }

    void Awake()
    {
        foreach (ItemCombinations item in StaticItemCombo)
        {
            AddToHash(item);
        }
        _inventory = _inspectorInventory;
    }
}
