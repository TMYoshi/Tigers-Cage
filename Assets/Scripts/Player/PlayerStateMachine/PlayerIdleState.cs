using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateManager context) : base(context)
    {
        _context = context;
    }

    Action LocateMovementController;
    Collider2D currentCollider;

    private void MoveToWalk()
    {
        _context.UpdatePlayerCharacterReference();

        if(_context._MovementController == null)
            return;

        PlayerController.WalkToOnClick(_context._MovementController);
    }

    public override void EnterState()
    {
        PlayerInput.Instance.MouseOnClickInput -= MoveToWalk;
        PlayerInput.Instance.MouseOnClickInput += MoveToWalk;

        PlayerInput.Instance.MouseOnClickInput -= ActualClick;
        PlayerInput.Instance.MouseOnClickInput += ActualClick;

        PlayerInput.Instance.InvOnClick -= PauseMenu.InvHandler;
        PlayerInput.Instance.InvOnClick += PauseMenu.InvHandler;
    }

    public override void UpdateState()
    {
        //if Mouse is over any UI, it will not click the world
        MouseDetection();
        UIMouseDetection();
    }

    public override void Cleanup()
    {
        PlayerInput.Instance.MouseOnClickInput -= MoveToWalk;
        PlayerInput.Instance.MouseOnClickInput -= ActualClick;
        PlayerInput.Instance.InvOnClick -= PauseMenu.InvHandler;
    }

    private void OnDisable()
    {
        Cleanup();
    }

    private void ActualClick()
    {
        currentCollider = _context._MouseUtils.JustReturnColliders();

        if(currentCollider == null) return;

        InventoryItem _inventoryItem = currentCollider.gameObject.GetComponent<InventoryItem>();
        _context._ItemManager.UpdateSelectedItem(_inventoryItem);

        _context.UpdatePlayerCharacterReference();

        switch (currentCollider.gameObject.tag)
        {
            case "Item":
                if(_context._MovementController != null)
                    _context._MovementController.MoveTo
                    (
                        Camera.main.ScreenToWorldPoint(PlayerInput.Instance.MouseInput),
                        () => _context?.UpdateCurrentState(PlayerStateManager.State.DialogItem)
                    );
                else
                    _context?.UpdateCurrentState(PlayerStateManager.State.DialogItem);
                break;
            case "SpecialItem":
                if(_context._MovementController != null)
                    _context._MovementController.MoveTo
                    (
                        Camera.main.ScreenToWorldPoint(PlayerInput.Instance.MouseInput),
                        () => _context?.UpdateCurrentState(PlayerStateManager.State.SpecialItem)
                    );
                else   
                    _context?.UpdateCurrentState(PlayerStateManager.State.SpecialItem);
                break;
            case "Transitions":
                ArrowController arrowController = currentCollider.gameObject.GetComponent<ArrowController>();
                if(_context._MovementController != null)
                    _context._MovementController.MoveTo
                    (
                        Camera.main.ScreenToWorldPoint(PlayerInput.Instance.MouseInput),
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

    public void MouseDetection()
    {
        Collider2D currentCollider = _context._MouseUtils.HighlightOnHover();
    }

    public void UIMouseDetection()
    {
        if (EventSystem.current == null)
            return;

        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = PlayerInput.Instance.MouseInput
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject == null)
            {
                continue;
            }
            switch (result.gameObject.tag)
            {
                case "InvItem":
                    _context.UpdateCurrentState(PlayerStateManager.State.Inventory);
                    break;
                default:
                    break;
            }
        }
    }
}


