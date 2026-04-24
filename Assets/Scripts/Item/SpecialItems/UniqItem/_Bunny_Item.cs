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
        if(documentToUnlock != null)
        {
            documentToUnlock.isUnlocked = true;
            Debug.Log($"Document '{documentToUnlock.documentTitle}' unlocked!");
        }
        return true;
    }
    public override bool ExitCondition()
    {
        playerAnimator.SetBunnyTrue();
        
       
        return true;
    }
}
