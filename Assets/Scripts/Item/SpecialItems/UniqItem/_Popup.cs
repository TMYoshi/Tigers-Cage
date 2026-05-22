using System.Collections.Generic;
using UnityEngine;

public class _Popup: SpecialItems
{
    public override void EnterCondition()
    {
    }

    public override bool CompleteCondition() 
    {
        return true;
    }

    public override bool ExitCondition()
    {
        PopupManager.Instance.SetpopupOnJournal();
        return true;
    }
}
