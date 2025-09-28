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
                _renderer.sprite = getDraggedObject().itemSprite;

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
            DetectWhenExit(InteractedItem);
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

    public void DetectWhenExit(GameObject detectedObject)
    {
        if (outlineScript != null) outlineScript.Exit();
        InventoryItem detectedInvItem = detectedObject?.GetComponent<InventoryItem>();
        if (detectedInvItem == null) ExitState();
        /*  the way interactions work in this game is that it
            allows items without the special item tag to be
            special items
        */
        ItemInteraction itemInteraction = detectedObject?.GetComponent<ItemInteraction>();
        if (itemInteraction == null && detectedObject != null)
        {
            Debug.Log("test");
            _context._ItemManager.HideDraggedItem();
            _context._ItemManager.UpdateSelectedItem(_context._ItemManager._FailedInteraction);
            _context.UpdateCurrentState(PlayerStateManager.State.DialogItem);
        }
    }
}
