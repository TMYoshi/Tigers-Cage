using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogSO", menuName = "Dialog/DialogNode")]
public class DialogSO : ScriptableObject
{
    public DialogLine[] lines_;

    [Header("Conditional Requirements (Optional)")]
    public string required_item_;

    // Lowkey this is gonna be really useful for triggering certain scenes as can trigger after certain events or interacting with certain objects!!
    public bool IsConditionMet()
    {
        if (required_item_ != Equip_Button.Instance.GetEquipped())
        {
            Debug.Log($"Failed Condition; not {required_item_}");
            return false;
        }
        Debug.Log("Passed Condition");
        return true;
    }

    public DialogLine[] GetLines()
    {
        return lines_;
    }
}

[System.Serializable]
public class DialogLine
{
    public ActorSO speaker_;
    [TextArea(3,5)] public string text_;

}
