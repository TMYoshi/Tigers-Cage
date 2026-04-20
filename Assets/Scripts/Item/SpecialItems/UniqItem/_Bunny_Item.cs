using UnityEngine;

public class _Bunny_Item : SpecialItems
{
    public override void EnterCondition()
    {
        Debug.Log("This is a bunny");
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
