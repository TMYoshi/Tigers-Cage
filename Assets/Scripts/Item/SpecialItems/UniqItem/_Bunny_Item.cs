using UnityEngine;

public class _Bunny_Item : SpecialItems
{
    [SerializeField] HeartbeatMinigame minigame;
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
        minigame.StartHeartBeatMinigame();

        if(documentToUnlock != null)
        {
            documentToUnlock.isUnlocked = true;
            Debug.Log($"Document '{documentToUnlock.documentTitle}' unlocked!");
        }
        return true;
    }
}
