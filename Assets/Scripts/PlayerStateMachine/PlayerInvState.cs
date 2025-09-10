using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class PlayerInvState : PlayerBaseState
{
    public PlayerInvState(PlayerStateManager context) : base(context)
    {
        _context = context;
    }
    public override void EnterState()
    {
        Time.timeScale = 0;
        _context._InventoryMenu.SetActive(true);
    }
    public override void UpdateState()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            ExitState();
        }
    }
    public override void ExitState()
    {
        Time.timeScale = 1;
        _context._InventoryMenu.SetActive(false); // deactivates menu screen
        _context.UpdateCurrentState(PlayerStateManager.State.Idle);
    }
}
