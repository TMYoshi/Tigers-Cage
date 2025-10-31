using UnityEngine;

public class _Ripped_Card : SpecialItems
{
    public override void EnterCondition()
    {
        Debug.Log("this is from FORK OUTLET");
    }
    public override bool CompleteCondition()
    {
        return false;
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
