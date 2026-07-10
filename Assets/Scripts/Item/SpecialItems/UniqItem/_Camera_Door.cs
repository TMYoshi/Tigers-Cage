using System.Collections;
using UnityEngine;

public class _Camera_Door: SpecialItems
{    public override void EnterCondition()
    {
    }

    public override bool CompleteCondition()
    {
        return true;
    }

    public override bool ExitCondition()
    {
        PopupManager.Instance.SetCurrPopup(PopupManager.Instance.door_exit_popup_);
        PopupManager.Instance.SetUIPopuopOn();

        StartCoroutine(WaitForPopupClosure());

        return true;
    }

    private IEnumerator WaitForPopupClosure()
    {
        yield return new WaitUntil(() => PlayerStateManager.Instance.GetCurrentState() is PlayerIdleState);

        PlayerStateManager.Instance.UpdateToNullState();
    }

    public void UpdatePlayerToIdleState() =>
        PlayerStateManager.Instance.UpdateToIdleState();

}
