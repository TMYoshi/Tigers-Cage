using UnityEngine;

public class _Bunny_Item : SpecialItems
{
    [SerializeField] PlayerAnimator playerAnimator;
    public override void EnterCondition()
    {
    }
    public override bool CompleteCondition() 
    {
        return true;
    }
    public override bool ExitCondition()
    {
        playerAnimator.SetBunnyTrue();
        return true;
    }
}
