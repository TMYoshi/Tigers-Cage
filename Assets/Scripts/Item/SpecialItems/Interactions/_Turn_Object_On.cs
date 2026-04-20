using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class _Turn_Object_On : SpecialItems
{
    [SerializeField] InventoryManager invManager;
    [SerializeField] GameObject objectToTurnOn;
    [SerializeField] string itemToRemove;
    public override void EnterCondition()
    {
    }
    public override bool CompleteCondition()
    {
        objectToTurnOn.SetActive(true);
        foreach(ItemSlot slot in invManager.itemSlot)
        {
            if(slot.itemName == itemToRemove)
            {
                slot.RemoveItem();
            }
        }

        return true;
    }
    public override bool ExitCondition()
    {
        return false;
    }

    public override void RewardCondition()
    {

    }

    public override void CleanUpCondition()
    {

    }
}
