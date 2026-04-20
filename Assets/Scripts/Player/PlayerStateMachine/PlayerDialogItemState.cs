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
        SpecialItems specialItem = _context._ItemManager._SelectedItem.GetSpecialEvents();

        if(specialItem != null)
            specialItem.EnterCondition();

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
            Cleanup();
            _context.UpdateCurrentState(PlayerStateManager.State.Idle);
    }

    public override void Cleanup()
    {
        if(_context._ItemManager._SelectedItem == null) return;

        if (_context._ItemManager._SelectedItem.Collectable)
        {
            MarkItemAsCollected(_context._ItemManager._SelectedItem);
            if(!_context._ItemManager._SelectedItem.Destroyable) AddItemToInv(_context._ItemManager._SelectedItem);
            _context._ItemManager.DestroySelectedItem();
        }
        _context._ItemManager.UpdateSelectedItem(null);
    }

    public static void AddItemToInv(InventoryItem _inventoryItem)
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

    static public void MarkItemAsCollected(InventoryItem _inventoryItem)
    {
        string itemId = GenerateItemId(_inventoryItem);
        InventoryManager.MarkItemAsCollected(itemId);
    }

    static public string GenerateItemId(InventoryItem _inventoryItem)
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        return sceneName + "_" + _inventoryItem.gameObject.name + "_" + _inventoryItem.ItemName;
    }
}
