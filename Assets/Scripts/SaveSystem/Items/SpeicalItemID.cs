using UnityEngine;

public class SpeicalItemID : MonoBehaviour
{
    [SerializeField] private string itemID;

    private void Start()
    {
        if (InventoryManager.IsItemCollected(itemID))
        {
            Destroy(gameObject);
        }
    }

    public void Collect()
    {
        InventoryManager.MarkItemAsCollected(itemID);
        Destroy(gameObject);
    }
}
