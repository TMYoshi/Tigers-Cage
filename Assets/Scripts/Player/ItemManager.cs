using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private InventoryItem _selectedItem;
    [SerializeField] private GameObject _draggedItem;

    public InventoryItem _SelectedItem => _selectedItem;
    public GameObject _DraggedItem => _draggedItem;

    public void UpdateSelectedItem(InventoryItem item) => _selectedItem = item;
    public void DestroySelectedItem() => Destroy(_selectedItem.gameObject);
    public void HideDraggedItem() => _DraggedItem.SetActive(false);
    public void ShowDraggedItem() => _DraggedItem.SetActive(true);
}
