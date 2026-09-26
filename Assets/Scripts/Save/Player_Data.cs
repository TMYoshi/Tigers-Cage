using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Player_Data
//can add puzzle, scene/position.
{
    //which scene to load
    public int sceneIndex;
    public List<InventorySlotData> inventorySlots;
    public List<string> collectedItemIds;
    public List<JournalPageSaveData> unlockedJournalEntries;


    public Player_Data(int currentSceneIndex, List<InventorySlotData> inventorySlots, List<string> collectedItemIds, List<JournalPageSaveData> unlockedJournalEntries)
    {
        //scene to load back into
        sceneIndex = currentSceneIndex;

        this.inventorySlots = inventorySlots;
        this.collectedItemIds = collectedItemIds;
        this.unlockedJournalEntries = unlockedJournalEntries;
        // bug inventorySlots = inventorySlots;
        //unlock items = unlockItems;
        //keybinds

    }

}

[Serializable]
public class JournalPageSaveData
{
    public string documentName;
    public int pageNumber;

    public JournalPageSaveData(string documentName, int pageNumber)
    {
        this.documentName = documentName;
        this.pageNumber = pageNumber;
    }
}


