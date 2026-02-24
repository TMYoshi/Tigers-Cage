using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

//slop code incoming ;-;
public class PlayerInvState : PlayerBaseState
{

    public PlayerInvState(PlayerStateManager context) : base(context)
    {
        _context = context;
    }
    public override void EnterState()
    {
        AlreadyClicked = false;
        InteractedItem = null;
    }
    RectTransform itemTransform;
    GameObject InteractedItem;
    bool AlreadyClicked;
    public override void UpdateState()
    {
        if (PlayerInput.Instance.MouseClickInput)
        {
            AlreadyClicked = true;
            if (!_context._ItemManager._DraggedItem.activeSelf)
            {
                _context._ItemManager.ShowDraggedItem();
                Image _renderer = _context._ItemManager._DraggedItem.GetComponent<Image>();
                itemTransform = _context._ItemManager._DraggedItem.GetComponent<RectTransform>();

                ItemSlot selectedItemSlot = getDraggedObject();
                if (_renderer?.sprite == null) { ExitState(); Debug.Log("<color=red>NO SPRITE IMAGE</color>"); return;}
                if (selectedItemSlot?.itemSprite == null) { ExitState(); Debug.Log("<color=red>NO SPRITE IMAGE</color>"); return;}

                _renderer.sprite = selectedItemSlot.itemSprite;
                _context._ItemManager._DraggedItem.name = selectedItemSlot.itemName;
            }
            Vector2 mousePos = Input.mousePosition;
            itemTransform.position = mousePos;

            //reminder to self to fix this we have MULTIPLE RAYCAST AAHHAHAHAH
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePos), Vector2.zero);
            InteractedItem = hit.collider?.gameObject;
        }
        else
        {
            DetectWhenExit();
        }
        _context._MouseUtils.HighlightOnHover();
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
        InventoryItem detectedInvItem = InteractedItem?.GetComponent<InventoryItem>();
        if (detectedInvItem == null) ExitState();
        UIMouseDetection();
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
    void UIMouseDetection()
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = PlayerInput.Instance.MouseInput
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (RaycastResult result in results)
            {
                switch (result.gameObject.tag)
                {
                    case "InvItem":
                        if (!AlreadyClicked) return;
                        ItemSlot slotInfo = result.gameObject?.GetComponent<ItemSlot>();
                        if (slotInfo == null) Debug.LogWarning("No itemSlot in inventory");
                        Debug.Log(slotInfo.itemName + " " + _context._ItemManager._DraggedItem.name);
                        CraftingManager.CraftIfComboExist(slotInfo.itemName, _context._ItemManager._DraggedItem.name);
                        break;
                    default:
                        break;
                }
            }
        }

    void FailedInteraction()
    {
        _context._ItemManager.HideDraggedItem();
        _context._ItemManager.UpdateToDummy();
        _context.UpdateCurrentState(PlayerStateManager.State.DialogItem);
        Debug.Log("tripped Failed interaction");
    }
}
