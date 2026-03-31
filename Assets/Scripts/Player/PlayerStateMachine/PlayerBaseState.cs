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
    public virtual void Cleanup()
    {
        Debug.Log("No Cleanup State currently");
    }
}
