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
                _context._ItemManager.ShowDraggedItem();
                Image _renderer = _context._ItemManager._DraggedItem.GetComponent<Image>();
                itemTransform = _context._ItemManager._DraggedItem.GetComponent<RectTransform>();

                ItemSlot selectedItemSlot = getDraggedObject();
                if (_renderer?.sprite == null) { ExitState(); return;}
                if (selectedItemSlot?.itemSprite == null) { ExitState(); return;}

                _renderer.sprite = selectedItemSlot.itemSprite;
                _context._ItemManager._DraggedItem.name = selectedItemSlot.itemName;
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
            if (result.gameObject.tag == "InvItem") return result.gameObject.GetComponent<ItemSlot>();
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
        bool shouldEnterFail = true;
        if (outlineScript != null) outlineScript.Exit();
        InventoryItem detectedInvItem = InteractedItem?.GetComponent<InventoryItem>();
        if (detectedInvItem == null) ExitState();
        /*  the way interactions work in this game is that it
            allows items without the special item tag to be
            special items

            this is using a dummy item inside of the player state
        */
        Interaction[] itemInteraction = InteractedItem?.GetComponents<Interaction>();
        if (itemInteraction != null || InteractedItem == null)
        {
            if (itemInteraction == null) return;
            foreach (Interaction interaction in itemInteraction)
            {
                if (interaction.key == _context._ItemManager._DraggedItem.name)
                {
                    //switches to special state after interaction
                    _context._ItemManager.HideDraggedItem();
                    _context._ItemManager.UpdateToDummy();
                    interaction.ExecuteEffect(_context);
                    shouldEnterFail = false;
                }
            }
        }
        if (shouldEnterFail) FailedInteraction();
    }

    void FailedInteraction()
    {
        _context._ItemManager.HideDraggedItem();
        _context._ItemManager.UpdateToDummy();
        _context.UpdateCurrentState(PlayerStateManager.State.DialogItem);
        Debug.Log("tripped Failed interaction");
    }
}
