using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class _Fork_Outlet : SpecialItems
{
    public override void EnterCondition()
    {
        //don't put naything here for interations ;-;
    }
    public override bool CompleteCondition()
    {
        FadeController.Instance.FadeAndLoad("GameOver");
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
