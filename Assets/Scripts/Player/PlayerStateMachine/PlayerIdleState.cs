using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

//note i am a terrible programmer feel free to fix anything you deem fit

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
        MouseDetection();
        UIMouseDetection();
    }
    public override void ExitState()
    {
    }


    HighlightInteractableOutline outlineScript;

    public void MouseDetection()
    {
        Collider2D currentCollider = _context._MouseUtils.HighlightOnHover();

        if(currentCollider == null) return;

        Debug.Log("Hit object: " + currentCollider.gameObject.name);
        InventoryItem _inventoryItem = currentCollider.gameObject.GetComponent<InventoryItem>();
        _context._ItemManager.UpdateSelectedItem(_inventoryItem);
        if (outlineScript != null) outlineScript.Exit();

        _context.UpdatePlayerCharacterReference();

        switch (currentCollider.gameObject.tag)
        {
            case "Item":
                if(_context._MovementController != null)
                    _context._MovementController.MoveTo
                    (
                        currentCollider.transform,
                        () => _context?.UpdateCurrentState(PlayerStateManager.State.DialogItem)
                    );
                else
                    _context?.UpdateCurrentState(PlayerStateManager.State.DialogItem);
                break;
            case "SpecialItem":
                if(_context._MovementController != null)
                    _context._MovementController.MoveTo
                    (
                        currentCollider.transform,
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
    public void UIMouseDetection()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
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


