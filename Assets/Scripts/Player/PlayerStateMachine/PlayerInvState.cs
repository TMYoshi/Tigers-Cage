using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

//slop code incoming ;-;
public class PlayerInvState : PlayerBaseState
{
    RectTransform itemTransform;
    GameObject InteractedItem;
    bool AlreadyClicked;

    public PlayerInvState(PlayerStateManager context) : base(context)
    {
        _context = context;
    }

    public override void EnterState()
    {
        AlreadyClicked = false;
        InteractedItem = null;
    }

    public override void UpdateState()
    {
        _context._MouseUtils.HighlightOnHoverInv();
        if (PlayerInput.Instance.MouseClickInput)
        {
            AlreadyClicked = true;
            if (!_context._ItemManager._DraggedItem.activeSelf)
            {
                ItemSlot selectedItemSlot = getDraggedObject();
                if(selectedItemSlot == null) return;
                if(selectedItemSlot.itemSprite == null) return;

                _context._ItemManager.ShowDraggedItem();
                Image _renderer = _context._ItemManager._DraggedItem.GetComponent<Image>();
                itemTransform = _context._ItemManager._DraggedItem.GetComponent<RectTransform>();

                _renderer.sprite = selectedItemSlot.itemSprite;
                _context._ItemManager._DraggedItem.name = selectedItemSlot.itemName;
            }
            Vector2 mousePos = PlayerInput.Instance.MouseInput;
            itemTransform.position = mousePos;
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
            position = PlayerInput.Instance.MouseInput
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.tag == "InvItem")
            {
                ItemSlot draggedItemSlot = result.gameObject.GetComponent<ItemSlot>();
                if(draggedItemSlot.itemName != "")
                    InteractedItem = result.gameObject;

                return draggedItemSlot;
            }
        }
        return null;
    }

    public override void ExitState()
    {
        _context.UpdateCurrentState(PlayerStateManager.State.Idle);
    }

    public override void Cleanup()
    {
        _context._ItemManager.HideDraggedItem();
    }

    public void DetectWhenExit()
    {
        bool shouldReturn = false;
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
                    shouldReturn = true;
                    return;
                default:
                    break;
            }
        }

        if(!shouldReturn) ExitState();

        UIMouseDetectionReleased();
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
                    return;
                }
            }
        }
        FailedInteraction();
    }

    void UIMouseDetectionReleased()
    {
        if (!AlreadyClicked) return;
        ItemSlot slotInfo = InteractedItem?.GetComponent<ItemSlot>();
        if (slotInfo == null)
        {
            Debug.LogWarning("No itemSlot in inventory");
            return;
        }
        if (_context._ItemManager._DraggedItem == null)
        {
            Debug.LogWarning("No _DraggedItem current exist");
            return;
        }
        CraftingManager.CraftIfComboExist(slotInfo.itemName, _context._ItemManager._DraggedItem.name);
    }

    void FailedInteraction()
    {
        _context._ItemManager.HideDraggedItem();
        _context._ItemManager.UpdateToDummy();
        _context.UpdateCurrentState(PlayerStateManager.State.DialogItem);
        Debug.Log("tripped Failed interaction");
    }
}
