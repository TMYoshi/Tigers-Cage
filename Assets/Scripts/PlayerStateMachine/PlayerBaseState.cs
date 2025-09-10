using UnityEngine;

public abstract class PlayerBaseState
{
    public PlayerStateManager _context;
    public PlayerBaseState(PlayerStateManager context)
    {
        _context = context;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}
