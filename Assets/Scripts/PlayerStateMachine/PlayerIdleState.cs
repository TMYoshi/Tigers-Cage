using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateManager context) : base(context)
    {
        _context = context;
    }
    public override void EnterState()
    {

    }
    public override void UpdateState()
    {
        //this state just listens for inputs from the player
        if (Input.GetButtonDown("Inventory"))
        {
            _context.UpdateCurrentState(PlayerStateManager.State.Inventory);
        }
    }
    public override void ExitState()
    {

    }
}
