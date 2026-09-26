using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class _Popup: SpecialItems
{
    public UnityEvent OnExitItem;
    public override void EnterCondition()
    {
    }

    public override bool CompleteCondition() 
    {
        return true;
    }

    public override bool ExitCondition()
    {
        return true;
    }

    public override PlayerStateManager.State DialogExitCondition()
    {
        OnExitItem.Invoke();
        return PlayerStateManager.State.Idle;
    }

    public void JournalPopup()
    {
        PopupManager.Instance.SetPopupOnMessage
            ("Press E to use Journal", ref PlayerInput.Instance.InvOnClick);
    }

    public void FlashlightPopup()
    {
        PopupManager.Instance.SetPopupOnMessage
            ("Press F to use Flashlight", ref PlayerInput.Instance.FlashInput);
    }
}
