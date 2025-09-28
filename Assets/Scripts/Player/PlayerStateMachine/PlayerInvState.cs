using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class PlayerInvState : PlayerBaseState
{

    public PlayerInvState(PlayerStateManager context) : base(context)
    {
        _context = context;
    }
    public override void EnterState()
    {

    }
    HighlightInteractableOutline outlineScript;
    RectTransform itemTransform;
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
        return new ItemSlot();
    }
    public override void ExitState()
    {
        _context._ItemManager.HideDraggedItem();
        _context.UpdateCurrentState(PlayerStateManager.State.Idle);
    }

    public void DetectWhenExit()
    {
        ExitState();
    }
}
