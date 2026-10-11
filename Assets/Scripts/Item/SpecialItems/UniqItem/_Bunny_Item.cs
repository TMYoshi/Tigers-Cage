using System.Collections;
using UnityEngine;

public class _Bunny_Item : SpecialItems
{
    [SerializeField] HeartbeatMinigame minigame;
    public override void EnterCondition()
    {
    }

    public override bool CompleteCondition() 
    {
        return true;
    }

    public override bool ExitCondition()
    {
        return true;
    }

    public override PlayerStateManager.State DialogExitCondition()
    {
        minigame.StartHeartBeatMinigame();
        return PlayerStateManager.State.Null;
    }

    public void UpdatePlayerToIdleState()
    {
        PlayerStateManager.Instance.UpdateCurrentState(PlayerStateManager.State.Idle);
    }

    public void UpdatePlayerToNervousState()
    {
        PlayerStateManager.Instance.UpdateCurrentState(PlayerStateManager.State.Nervous);
    }

    public void PlaySelectedDialogAndSetPickupTrue(InventoryItem _InventoryItem)
    {
        PlayerStateManager.Instance.UpdateToDialogAndSpeak(_InventoryItem);
    }
}
