using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private InventoryItem _selectedItem;
    [SerializeField] private GameObject _draggedItem;
    [Header("Static Items (These items don't change)")]
    [SerializeField] private InventoryItem _dummyItem;
    [SerializeField] private InventoryItem _dialogItem;

    public InventoryItem _SelectedItem => _selectedItem;
    public InventoryItem _DummyItem => _dummyItem;
    public GameObject _DraggedItem => _draggedItem;

    public void UpdateSelectedItem(InventoryItem item) => _selectedItem = item;
    public void UpdateToDummy() => _selectedItem = _dummyItem;
    public void UpdateDialogItem(Dialog dialog)
    {
        _selectedItem = _dialogItem;
        _dialogItem.assoc_dialog_box_ = dialog;
    }
    public void DestroySelectedItem() => Destroy(_selectedItem.gameObject);
    public void HideDraggedItem() => _DraggedItem.SetActive(false);
    public void ShowDraggedItem() => _DraggedItem.SetActive(true);
}
