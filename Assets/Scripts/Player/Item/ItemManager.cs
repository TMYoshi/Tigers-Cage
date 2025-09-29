using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private InventoryItem _selectedItem;
    [SerializeField] private GameObject _draggedItem;
    [Header("Static Items (These items don't change)")]
    [SerializeField] private InventoryItem _dummyItem;

    public InventoryItem _SelectedItem => _selectedItem;
    public InventoryItem _DummyItem => _dummyItem;
    public GameObject _DraggedItem => _draggedItem;

    public void UpdateSelectedItem(InventoryItem item) => _selectedItem = item;
    public void UpdateToDummy() => _selectedItem = _dummyItem;
    public void DestroySelectedItem() => Destroy(_selectedItem.gameObject);
    public void HideDraggedItem() => _DraggedItem.SetActive(false);
    public void ShowDraggedItem() => _DraggedItem.SetActive(true);
}
