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
        if (string.IsNullOrEmpty (required_item_))
        {
            return true;
        }

        if (Equip_Button.Instance == null)
        {
            Debug.LogWarning("Equip_Button.Instance is null.");
            Equip_Button.Instance = FindFirstObjectByType<Equip_Button>();
        }

        if(Equip_Button.Instance == null)
        {
            Debug.LogError("No Equip_Button found.");
            return false;
        }

        return required_item_ == Equip_Button.Instance.GetEquipped();
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
