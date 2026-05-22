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
        OnExitItem.Invoke();
        return true;
    }

    public void JournalPopup()
    {
        PopupManager.Instance.SetpopupOnJournal();
    }

    public void FlashlightPopup()
    {
        PopupManager.Instance.SetpopupOnFlashlight();
    }
}
