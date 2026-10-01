using System.Collections.Generic;
using UnityEngine;

public class _BrokenCardPieces: SpecialItems
{
    [SerializeField] List<DialogSO> secondDialog;
    [SerializeField] List<DialogSO> lastDialog;
    public override void EnterCondition()
    {
    }

    public override void DialogEnterCondition()
    {
        Dialog currentDialog = GetComponent<Dialog>();
        if(currentDialog == null)
            Debug.Log("no dialog found in gameobject");

        if(!InventoryManager.AlreadyInteractedItems.Contains("BrokenCard1"))
            InventoryManager.AddAlreadyInteractedItem("BrokenCard1");

        else if(!InventoryManager.AlreadyInteractedItems.Contains("BrokenCard2"))
        {
            currentDialog.convos_ = secondDialog;
            InventoryManager.AddAlreadyInteractedItem("BrokenCard2");
        }

        else if(!InventoryManager.AlreadyInteractedItems.Contains("BrokenCard3"))
        {
            currentDialog.convos_ = lastDialog;
            InventoryManager.AddAlreadyInteractedItem("BrokenCard3");
        }
    }

    public override bool CompleteCondition() 
    {
        return false;
    }
    public override bool ExitCondition()
    {
        return false;
    }
}
