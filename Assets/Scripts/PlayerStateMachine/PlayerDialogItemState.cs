using UnityEditor;
using UnityEngine;

public class PlayerDialogItemState : PlayerBaseState
{
    public PlayerDialogItemState(PlayerStateManager context) : base(context)
    {
        _context = context;
    }

    public override void EnterState()
    {
        if (_context._CurrentItem.lines_.Count <= 0) ExitState();
        else _context._CurrentItem.WriteLines();
    }

    public override void UpdateState()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            if (_context._CurrentItem.WriteLines())
            {
                ExitState();
            }
        }
    }

    public override void ExitState()
    {
        if (_context._CurrentItem.Collectable)
        {
            MarkItemAsCollected(_context._CurrentItem);
            AddItemToInv(_context._CurrentItem);
            Object.Destroy(_context._CurrentItem.gameObject);
        }
        _context.UpdateCurrentState(PlayerStateManager.State.Idle);
    }

    void AddItemToInv(InventoryItem _inventoryItem)
    {
        InventoryManager inventoryManager = GameObject.Find("InventoryCanvas")?.GetComponent<InventoryManager>();
        if (inventoryManager != null)
        {
            inventoryManager.AddItem(
                _inventoryItem.ItemName,
                _inventoryItem.Quantity,
                _inventoryItem.Sprite,
                _inventoryItem.ItemDescription
            );
        }
        else
        {
            Debug.LogError("InventoryManager not found!");
        }
    }

    void MarkItemAsCollected(InventoryItem _inventoryItem)
    {
        string itemId = GenerateItemId(_inventoryItem);
        InventoryManager.MarkItemAsCollected(itemId);
    }

    string GenerateItemId(InventoryItem _inventoryItem)
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        return sceneName + "_" + _inventoryItem.gameObject.name + "_" + _inventoryItem.ItemName;
    }
}