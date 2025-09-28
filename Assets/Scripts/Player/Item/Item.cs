using UnityEngine;
using System.Collections.Generic;

public class Item : MonoBehaviour
{
    public Dialog assoc_dialog_box_;

    private float LastInteractionTime = 0f;
    private float InteractionCooldown = 0.2f; // .2s

    public bool WriteLines()
    {
        if (Time.time - LastInteractionTime < InteractionCooldown)
        {
            return false;
        }
        LastInteractionTime = Time.time;

        bool dialog_finished = assoc_dialog_box_.WriteDialog();
        // bool dialog_finished = assoc_dialog_box_.PlayDialog(dialog_so_);

        if (dialog_finished)
        {
            InventoryItem _inventoryItem = this as InventoryItem;
            if(_inventoryItem != null && _inventoryItem.Collectable)
            {
                Destroy(gameObject); // deleting item
            }
        }
        return dialog_finished;
    }
}
