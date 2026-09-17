using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData
//can add puzzle, scene/position.
{
    //which scene to load
    public int SceneIndex;
    public List<InventorySlotData> InventorySlots;
    public List<string> CollectedItemIds;
    public List<JournalPageSaveData> UnlockedJournalEntries;


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


