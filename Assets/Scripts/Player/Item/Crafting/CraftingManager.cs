using UnityEngine;
using System.Collections.Generic;

public class CraftingManager : MonoBehaviour
{
    public ItemCombinations[] StaticItemCombo;
    public static HashSet<ItemCombinations> CraftingSet = new HashSet<ItemCombinations>();
    public static void CheckIfComboExist(string item1, string item2)
    {
        foreach (ItemCombinations item in CraftingSet)
        {
            item.CraftCombo(item1, item2);
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
    }
}
