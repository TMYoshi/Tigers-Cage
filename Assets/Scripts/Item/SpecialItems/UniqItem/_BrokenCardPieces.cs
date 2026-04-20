using System.Collections.Generic;
using UnityEngine;

public class _BrokenCardPieces: SpecialItems
{
    [SerializeField] List<DialogSO> secondDialog;
    [SerializeField] List<DialogSO> lastDialog;
    public override void EnterCondition()
    {
        Dialog currentDialog = GetComponent<Dialog>();
        if(currentDialog == null)
            Debug.Log("no dialog found in gameobject");

        if(!InventoryManager.alreadyInteratedItems.Contains("BrokenCard1"))
            InventoryManager.alreadyInteratedItems.Add("BrokenCard1");

        else if(!InventoryManager.alreadyInteratedItems.Contains("BrokenCard2"))
        {
            currentDialog.convos_ = secondDialog;
            InventoryManager.alreadyInteratedItems.Add("BrokenCard2");
        }

        else if(!InventoryManager.alreadyInteratedItems.Contains("BrokenCard3"))
        {
            currentDialog.convos_ = lastDialog;
            InventoryManager.alreadyInteratedItems.Add("BrokenCard3");
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
