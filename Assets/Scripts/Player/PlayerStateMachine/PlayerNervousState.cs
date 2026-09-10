using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;
using UnityEngine;


public class PlayerNervousState : PlayerBaseState
{
    public PlayerNervousState(PlayerStateManager context) : base(context)
    {
        _context = context;
    }

    Action LocateMovementController;

    private void MoveToWalk()
    {
        if(_context._MovementController == null)
        {
            _context.UpdatePlayerCharacterReference();
            return;
        }

        PlayerController.WalkToOnClick(_context._MovementController);
    }

    public override void EnterState()
    {
        PlayerInput.Instance.MouseOnClickInput -= MoveToWalk;
        PlayerInput.Instance.MouseOnClickInput += MoveToWalk;
    }

    public override void UpdateState()
    {
        if (PauseMenu.isPaused)
            return;
        
        MouseDetection();
    }

    public override void Cleanup()
    {
        PlayerInput.Instance.MouseOnClickInput -= MoveToWalk;
    }

    private void OnDisable()
    {
        Cleanup();
    }

    public void MouseDetection()
    {
        Collider2D currentCollider = _context._MouseUtils.HighlightOnHover();

        if(currentCollider == null) return;

        InventoryItem _inventoryItem = currentCollider.gameObject.GetComponent<InventoryItem>();
        _context._ItemManager.UpdateSelectedItem(_inventoryItem);

        _context.UpdatePlayerCharacterReference();

        switch (currentCollider.gameObject.tag)
        {
            case "Item":
                if(currentCollider.gameObject.name != "Rabbit") return;
                if(_context._MovementController != null)
                    _context._MovementController.MoveTo
                    (
                        currentCollider.transform,
                        () => _context?.UpdateCurrentState(PlayerStateManager.State.DialogItem)
                    );
                else
                    _context?.UpdateCurrentState(PlayerStateManager.State.DialogItem);
                break;
            case "Transitions":
                ArrowController arrowController = currentCollider.gameObject.GetComponent<ArrowController>();
                if(_context._MovementController != null)
                    _context._MovementController.MoveTo
                    (
                        currentCollider.transform,
                        () => arrowController.OnPressed()
                    );
                else   
                    arrowController.OnPressed();
                break;
            default:
                Debug.Log("Hit non-item object: " + currentCollider.gameObject.name);
                break;
        }
    }
}


