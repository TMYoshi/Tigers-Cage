using System.Collections;
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
        PopupManager.Instance.SetCurrPopup(PopupManager.Instance.hb_minigame_popup_);
        PopupManager.Instance.SetUIPopuopOn();

        StartCoroutine(WaitForPopupClosure());

        return true;
    }

    private IEnumerator WaitForPopupClosure()
    {
        yield return new WaitUntil(() => PlayerStateManager.Instance.GetCurrentState() is PlayerIdleState);

        minigame.StartHeartBeatMinigame();
        PlayerStateManager.Instance.UpdateCurrentState(PlayerStateManager.State.Null);

        if(documentToUnlock != null)
        {
            documentToUnlock.isUnlocked = true;
            Debug.Log($"Document '{documentToUnlock.documentTitle}' unlocked!");
        }
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
