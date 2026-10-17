using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private InventoryItem _selectedItem;
    [SerializeField] private GameObject _draggedItem;
    [Header("Static Items (These items don't change)")]
    [SerializeField] private InventoryItem _dummyItemFailed;
    [SerializeField] private InventoryItem _dummyItemFull;
    [SerializeField] private InventoryItem _dialogItem;

    public InventoryItem _SelectedItem => _selectedItem;
    public InventoryItem _DummyItemFailed => _dummyItemFailed;
    public InventoryItem _DummyItemFull => _dummyItemFull;
    public GameObject _DraggedItem => _draggedItem;

    public void UpdateSelectedItem(InventoryItem item) => _selectedItem = item;
    public void UpdateToDummyFailed() => _selectedItem = _dummyItemFailed;
    public void UpdateToDummyFull() => _selectedItem = _dummyItemFull;
    public void UpdateDialogItem(Dialog dialog)
    {
        _selectedItem = _dialogItem;
        _dialogItem.assoc_dialog_box_ = dialog;
    }
    public void DestroySelectedItem() => Destroy(_selectedItem.gameObject);
    public void HideDraggedItem()
    {
        if(_DraggedItem == null) return;
        _DraggedItem.SetActive(false);
    }
    public void ShowDraggedItem()
    {
        if(_DraggedItem == null) return;
        _DraggedItem.SetActive(true);
    }
}
