using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData
//can add puzzle, scene/position.
{
    public string SceneIndex;
    public List<InventorySlotData> InventorySlots;
    public List<string> CollectedItemIds;
    public List<JournalPageSaveData> UnlockedJournalEntries;
    
    //add this back if we're bringing back flashlight
    //public Dictionary<string, bool> PlayerUpgrades;


    public PlayerData (SaveData _save)
    {
        //scene to load back into
        SceneIndex = _save.SceneIndex;
        InventorySlots = _save.InventorySlots;
        CollectedItemIds = _save.CollectedItemIds;
        UnlockedJournalEntries = _save.UnlockedJournalEntries;
        
        // bug inventorySlots = inventorySlots;
        //unlock items = unlockItems;
        //keybinds
    }
}


