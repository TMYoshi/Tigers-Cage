
using UnityEngine;

public class PlayerDialogItemState : PlayerBaseState
{
    public PlayerDialogItemState(PlayerStateManager context) : base(context)
    {
        _context = context;
    }

    bool isFull = false;

    public override void EnterState()
    {
        SpecialItems specialItem = _context._ItemManager._SelectedItem.GetSpecialEvents();

        if(specialItem != null)
            specialItem.DialogEnterCondition();

        isFull = false;
        if (_context._ItemManager._SelectedItem.Collectable)
        {
            if(!_context._ItemManager._SelectedItem.Destroyable) 
                isFull = !InventoryManager.IsInventoryFull();

            if (isFull)
            {
                _context._ItemManager.UpdateToDummyFull();
            }
        }

        _context._ItemManager._SelectedItem.WriteLines();

        if (SFXManager.Instance != null && !isFull)
            SFXManager.Instance.PlaySFXClip(_context._ItemManager._SelectedItem.AudioClip, _context.transform, 1f);

        PlayerInput.Instance.MouseOnClickInput -= DialogPlays;
        PlayerInput.Instance.MouseOnClickInput += DialogPlays;
    }

    private void DialogPlays()
    {
        if(_context._ItemManager._SelectedItem == null)
            _context.UpdateCurrentState(PlayerStateManager.State.Idle);

        if (_context._ItemManager._SelectedItem.WriteLines())
        {
            if(_context._ItemManager._SelectedItem == null) return;

            PlayerStateManager.State nextState = PlayerStateManager.State.Idle;

            SpecialItems specialItem = _context._ItemManager._SelectedItem.GetSpecialEvents();
            if(specialItem != null)
                nextState = specialItem.DialogExitCondition();

            _context.UpdateCurrentState(nextState);
        }
    }

    public override void UpdateState()
    {
    }

    public override void Cleanup()
    {
        PlayerInput.Instance.MouseOnClickInput -= DialogPlays;
        if(_context._ItemManager._SelectedItem == null) return;

        JournalCollectable journalItem = _context._ItemManager._SelectedItem.GetComponent<JournalCollectable>();
        
        if(journalItem != null)
        {
            Debug.Log("JournalCollectable found, adding to journal.");
            journalItem.AddtoJournal();
        }
        else
        {
            Debug.Log("No JournalCollectable found, item will not be added to journal.");
        }

        if (_context._ItemManager._SelectedItem.Collectable)
        {
            if(!_context._ItemManager._SelectedItem.Destroyable) 
            {
                //the reason I have duplicated code is I only want the item to disapeer when the player finishes interacting
                InventoryManager.AddItemToInv(_context._ItemManager._SelectedItem);
            }

            MarkItemAsCollected(_context._ItemManager._SelectedItem);
            _context._ItemManager.DestroySelectedItem();
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
