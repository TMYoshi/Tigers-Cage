using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public void SaveGame()
    {
        Player_Data data = new Player_Data();

        foreach (var slot in inventoryManager.itemSlot)
        {
            if (slot.isFull)
            {
                //copy item from ui itme into save slots
                Player_Data.Inventorydata d = new Player_Data.Inventorydata();
                d.itemname = slot.itemName;
                d.quantity = slot.quantity;
                d.descritption = slot.itemDescription;
                data.inventorySlots.Add(d);

            }
        }
        //send player_data into the file saving system
        Saves_System.SavePlayer(data);
    }

    public void LoadGame()
    {
        //return the saved data if not return non
        Player_Data data = Saves_System.LoadPlayer();
        if (data == null)
        {
            return;
        }
        //clear all curretn inventory slots before adding them
        foreach (var slot in inventoryManager.itemSlot)
        {
            //clear evrytinhf
        }
        //load  each saved slot back into the UI
        for (int i = 0; i < data.inventorySlots.Count; i++)
        {
            var s = data.inventorySlots[i];
            //inventoryManager.itemSlot[i].AddItem(d.itemname, s.quantity, d.descritption);
        }
    }
}
