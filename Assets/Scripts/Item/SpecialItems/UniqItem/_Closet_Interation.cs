using UnityEngine;

public class _Closet_Interation : SpecialItems 
{
    [SerializeField] ClosetScript closetLogic;
    public override void EnterCondition()
    {
        closetLogic.ToggleCloset();
    }
    public override bool CompleteCondition() 
    {
        return true;
    }
    public override bool ExitCondition()
    {
        return true;
    }
}
