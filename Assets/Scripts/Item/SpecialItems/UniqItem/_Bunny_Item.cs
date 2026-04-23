using UnityEngine;

public class _Bunny_Item : SpecialItems
{
    [SerializeField] PlayerAnimator playerAnimator;
    public DocumentItem documentToUnlock;
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
        
        if(documentToUnlock != null)
        {
            documentToUnlock.isUnlocked = true;
            JournalDataManager.Instance.SaveProgress();
        }
        return true;
    }
}
