using System.Collections.Generic;
using UnityEngine;

public class _Completed_Card : SpecialItems
{
    [SerializeField] GameObject brokenCard;
    [SerializeField] GameObject finishedCard;
    [SerializeField] List<DialogSO> puttingCardTogetherDialog;
    void Start()
    {
        if(InventoryManager.alreadyInteratedItems.Contains("Completed Card"))
            RewardCondition();
    }

    public override void EnterCondition()
    {
        if(!InventoryManager.alreadyInteratedItems.Contains("Put Ripped Card 1")) return;
        if(!InventoryManager.alreadyInteratedItems.Contains("Put Ripped Card 2")) return;
        if(!InventoryManager.alreadyInteratedItems.Contains("Put Ripped Card 3")) return;

        Dialog currentDialog = GetComponent<Dialog>();
        if(currentDialog == null)
            Debug.Log("no dialog found in gameobject");

        currentDialog.convos_ = puttingCardTogetherDialog;

        InventoryManager.alreadyInteratedItems.Add("Completed Card");

        RewardCondition();
    }

    public override void RewardCondition()
    {
        brokenCard.SetActive(false);
        finishedCard.SetActive(true);
    }

    public override bool CompleteCondition() 
    {
        return true;
    }
    public override bool ExitCondition()
    {
        return true;
    }
}
