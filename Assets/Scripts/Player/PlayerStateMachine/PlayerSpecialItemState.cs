using UnityEngine;

public class PlayerSpecialItemState : PlayerBaseState
{
    SpecialItems specialItem;
    public PlayerSpecialItemState(PlayerStateManager context) : base(context)
    {
        _context = context;
    }
    public override void EnterState()
    {
        SFXManager.Instance.PlaySFXClip(_context._ItemManager._SelectedItem.AudioClip, _context.transform, 1f);
        specialItem = _context._ItemManager._SelectedItem.GetSpecialEvents();
        Debug.Log("special Item Enter Condition");
        specialItem.EnterCondition();
    }
    public override void UpdateState()
    {
        if (specialItem.CompleteCondition())
        {
            ExitState();
        }

        if (specialItem.ExitCondition())
        {
            specialItem.CleanUpCondition();
            _context.UpdateCurrentState(PlayerStateManager.State.Idle);
        }
    }
    public override void ExitState()
    {
        specialItem.CleanUpCondition();
        specialItem.RewardCondition();
        _context.UpdateCurrentState(PlayerStateManager.State.Idle);
    }
}
