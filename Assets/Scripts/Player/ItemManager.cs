using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private InventoryItem _selectedItem;
    [SerializeField] private InventoryItem _currentlyUsing;

    public InventoryItem _SelectedItem => _selectedItem;
    public InventoryItem _CurrentlyUsing => _currentlyUsing;

    public void UpdateSelectedItem(InventoryItem item) => _selectedItem = item;
    public void DestroySelectedItem() => Destroy(_selectedItem.gameObject);
    public void UpdateCurrentlyUsing(InventoryItem item) => _selectedItem = item;
}
