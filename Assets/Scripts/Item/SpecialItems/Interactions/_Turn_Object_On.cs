using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class _Turn_Object_On : SpecialItems
{
    [SerializeField] GameObject objectToTurnOn;
    [SerializeField] string itemToRemove;
    [SerializeField] string saveKey;
    void Start()
    {
        if(InventoryManager.alreadyInteratedItems.Contains(saveKey))
            objectToTurnOn.SetActive(true);
    }

    public override void EnterCondition()
    {
    }
    public override bool CompleteCondition()
    {
        foreach(ItemSlot slot in InventoryManager.Instance.itemSlot)
        {
            if(slot.itemName == itemToRemove)
            {
                objectToTurnOn.SetActive(true);
                slot.RemoveItem();
                InventoryManager.alreadyInteratedItems.Add(saveKey);
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
