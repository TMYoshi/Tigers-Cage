using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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
        _context.UpdatePlayerCharacterReference();

        if(_context._MovementController == null)
            return;

        PlayerController.WalkToOnClick(_context._MovementController);
    }

    void StressedUpOnSceneChange(Scene _, Scene __)
    {
        if(HeartbeatBackground.Instance != null)
            HeartbeatBackground.TurnStressUp();
    }

    public override void EnterState()
    {
        PlayerInput.Instance.MouseOnClickInput -= MoveToWalk;
        PlayerInput.Instance.MouseOnClickInput += MoveToWalk;

        SceneManager.activeSceneChanged -= StressedUpOnSceneChange;
        SceneManager.activeSceneChanged += StressedUpOnSceneChange;

        if(HeartbeatBackground.Instance != null)
            HeartbeatBackground.TurnStressUp();
    }

    public override void UpdateState()
    {
        if (PauseMenu.isPaused)
            return;
        
        MouseDetection();
    }

    public override void Cleanup()
    {
        HeartbeatBackground.TurnStressDown();

        PlayerInput.Instance.MouseOnClickInput -= MoveToWalk;
        SceneManager.activeSceneChanged -= StressedUpOnSceneChange;
    }

    private void OnDisable()
    {
        Cleanup();
    }

    bool CheckTransitionAndRabbit(RaycastHit2D _hit)
    {
        if(_hit.collider.gameObject.tag == "Transitions" || _hit.collider.gameObject.name == "Rabbit")
            return true;
        else
            return false;
    }

    public void MouseDetection()
    {
        Collider2D currentCollider = _context._MouseUtils.HighlightOnCondition(CheckTransitionAndRabbit);

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


