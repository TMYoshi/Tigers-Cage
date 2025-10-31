using UnityEngine;

public class DialogItemInteraction : Interaction
{
    public Dialog dialog;
    public override void ExecuteEffect(PlayerStateManager _context)
    {
        _context._ItemManager.UpdateDialogItem(dialog);
        _context.UpdateCurrentState(PlayerStateManager.State.DialogItem);
    }
}

