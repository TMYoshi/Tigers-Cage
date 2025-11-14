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
        if (!Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                // If we're hovering a new object, exit the old one
                HighlightInteractableOutline newOutline = hit.collider.gameObject.GetComponent<HighlightInteractableOutline>();
                if (outlineScript == newOutline) return;
                if (outlineScript != null) outlineScript.Exit();

                outlineScript = newOutline;

                if (outlineScript != null) outlineScript.Enter();
            }
            else
            {
                // Not hovering anything, exit previous outline
                if (outlineScript == null) return;

                outlineScript.Exit();
                outlineScript = null;
            }
        }
        else
        {
            Debug.Log("Mouse clicked somewhere");
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Collider2D currentCollider = hit.collider;

                Debug.Log("Hit object: " + currentCollider.gameObject.name);
                InventoryItem _inventoryItem = currentCollider.gameObject.GetComponent<InventoryItem>();
                _context._ItemManager.UpdateSelectedItem(_inventoryItem);
                if (outlineScript != null) outlineScript.Exit();

                _context.UpdatePlayerCharacterReference();

                switch (currentCollider.gameObject.tag)
                {
                    case "Item":
                        _context._MovementController.MoveTo
                        (
                            currentCollider.transform,
                            () => _context?.UpdateCurrentState(PlayerStateManager.State.DialogItem)
                        );
                        break;
                    case "SpecialItem":
                        _context._MovementController.MoveTo
                        (
                            currentCollider.transform,
                            () => _context?.UpdateCurrentState(PlayerStateManager.State.SpecialItem)
                        );
                        break;
                    case "Transitions":
                        ArrowController arrowController = currentCollider.gameObject.GetComponent<ArrowController>();
                        arrowController.OnPressed();
                        break;
                    default:
                        Debug.Log("Hit non-item object: " + currentCollider.gameObject.name);
                        break;
                }
            }
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


