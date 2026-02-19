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


    public Player_Data(int currentSceneIndex, List<InventorySlotData> inventorySlots, List<string> collectedItemIds)
    {
        //scene to load back into
        sceneIndex = currentSceneIndex;

        this.inventorySlots = inventorySlots;
        this.collectedItemIds = collectedItemIds;
        // bug inventorySlots = inventorySlots;

    }

}


