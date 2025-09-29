using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

public class PlayerInvState : PlayerBaseState
{

    public PlayerInvState(PlayerStateManager context) : base(context)
    {
        _context = context;
    }
    public override void EnterState()
    {
        InteractedItem = null;
    }
    HighlightInteractableOutline outlineScript;
    RectTransform itemTransform;
    GameObject InteractedItem;
    public override void UpdateState()
    {
        if (Input.GetMouseButton(0))
        {
            if (!_context._ItemManager._DraggedItem.activeSelf)
            {
                _context._ItemManager._DraggedItem.SetActive(true);
                Image _renderer = _context._ItemManager._DraggedItem.GetComponent<Image>();
                itemTransform = _context._ItemManager._DraggedItem.GetComponent<RectTransform>();

                ItemSlot selectedItemSlot = getDraggedObject();
                _renderer.sprite = selectedItemSlot.itemSprite;
                _context._ItemManager._DraggedItem.name = selectedItemSlot.itemName;

                if (_renderer.sprite == null) ExitState();
            }

            Vector2 mousePos = Input.mousePosition;
            itemTransform.position = mousePos;

            //highlight stuff
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePos), Vector2.zero);

            if (hit.collider != null)
            {
                // If we're hovering a new object, exit the old one
                HighlightInteractableOutline newOutline = hit.collider.gameObject.GetComponent<HighlightInteractableOutline>();
                InteractedItem = hit.collider.gameObject;
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
                InteractedItem = null;
            }
        }
        else
        {
            DetectWhenExit();
        }
    }

    ItemSlot getDraggedObject()
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
                    return result.gameObject.GetComponent<ItemSlot>();
                default:
                    break;
            }
        }
        return null;
    }
    public override void ExitState()
    {
        _context._ItemManager.HideDraggedItem();
        _context.UpdateCurrentState(PlayerStateManager.State.Idle);
    }

    public void DetectWhenExit()
    {
        if (outlineScript != null) outlineScript.Exit();
        InventoryItem detectedInvItem = InteractedItem?.GetComponent<InventoryItem>();
        if (detectedInvItem == null) ExitState();
        /*  the way interactions work in this game is that it
            allows items without the special item tag to be
            special items

            this is using a dummy item inside of the player state
        */
        ItemInteraction itemInteraction = InteractedItem?.GetComponent<ItemInteraction>();
        if (itemInteraction != null || InteractedItem == null)
        {
            if (itemInteraction == null) return;
            for (int I = 0; I < itemInteraction._Interactions.Count; I++)
            {
                if (itemInteraction._Interactions[I].key == _context._ItemManager._DraggedItem.name)
                {
                    //switches to special state after interaction
                    _context._ItemManager.HideDraggedItem();
                    _context._ItemManager.UpdateToDummy();
                    SpecialItems specialToAssign = itemInteraction._Interactions[I].effect;
                    _context._ItemManager._SelectedItem.AssignSpecialEvents(specialToAssign);
                    _context.UpdateCurrentState(PlayerStateManager.State.SpecialItem);
                }
            }
        }
        else
        {
            FailedInteraction();
        }
    }

    void FailedInteraction()
    {
        _context._ItemManager.HideDraggedItem();
        _context._ItemManager.UpdateToDummy();
        _context.UpdateCurrentState(PlayerStateManager.State.DialogItem);
    }
}
