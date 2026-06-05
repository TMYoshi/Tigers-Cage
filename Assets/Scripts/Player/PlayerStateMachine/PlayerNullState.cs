using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;
using UnityEngine;

//state to lock players from certain actions
public class PlayerNullState : PlayerBaseState
{
    public PlayerNullState(PlayerStateManager context) : base(context)
    {
        _context = context;
    }

    public override void EnterState()
    {
    }
    public override void UpdateState()
    {
    }
    public override void ExitState()
    {
    }

    public override void Cleanup()
    {
    }
}


