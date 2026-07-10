using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class _Camera_Door: SpecialItems
{
    [SerializeField] private string requiredItemKey = "WorkingCamera";
    [SerializeField] private DialogSO doorDialog;

    public override void EnterCondition()
    {
        Debug.Log("exit called");
        if (doorDialog != null)
        {
            DialogManager.Instance.StartDialog(doorDialog);
        }
    }

    public override bool CompleteCondition()
    {
        return !DialogManager.Instance.is_dialog_active_;
    }

    public override bool ExitCondition()
    {
        InventoryManager.DebugPrintAllCollectedItems();

        bool isCollected = InventoryManager.IsItemCollected(requiredItemKey);
        Debug.Log("have camera: " + InventoryManager.IsItemCollected(requiredItemKey));
        Debug.Log("key owned: " + requiredItemKey + isCollected);

        if (isCollected)
        {
            Debug.Log("item found, showing popup.");
            PopupManager.Instance.SetCurrPopup(PopupManager.Instance.door_exit_popup_);
            PopupManager.Instance.SetUIPopuopOn();
        }
        else
        {
            Debug.Log("camera missing");
        }
        return true;
    }

    public void UpdatePlayerToIdleState() =>
        PlayerStateManager.Instance.UpdateToIdleState();

}
