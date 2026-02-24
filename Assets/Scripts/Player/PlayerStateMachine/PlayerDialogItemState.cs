using NUnit.Framework.Internal;
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
        _context._ItemManager._SelectedItem.WriteLines();

        if (SFXManager.Instance == null) return;
        SFXManager.Instance.PlaySFXClip(_context._ItemManager._SelectedItem.AudioClip, _context.transform, 1f);
    }

    public override void UpdateState()
    {
        if (PlayerInput.Instance.MouseClickInput) // Left mouse button
        {
            if (_context._ItemManager._SelectedItem.WriteLines())
            {
                ExitState();
            }
        }
    }

    public override void ExitState()
    {
        if (_context._ItemManager._SelectedItem.Collectable)
        {
            MarkItemAsCollected(_context._ItemManager._SelectedItem);
            AddItemToInv(_context._ItemManager._SelectedItem);
            _context._ItemManager.DestroySelectedItem();
        }
        _context._ItemManager.UpdateSelectedItem(null);
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
