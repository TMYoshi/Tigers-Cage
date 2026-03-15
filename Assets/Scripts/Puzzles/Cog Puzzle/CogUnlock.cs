using UnityEngine;

public class CogUnlock : MonoBehaviour
{
    [SerializeField] private GameObject mediumLockedCog;
    [SerializeField] private GameObject largeLockedCog;

    private void Start()
    {
        CheckAndUnlock("Medium Cog", mediumLockedCog);
        CheckAndUnlock("Large Cog", largeLockedCog);
    }

    private void CheckAndUnlock(string itemName, GameObject cogObject)
    {
        bool hasItem = false;

        foreach (string id in InventoryManager.collectedItems)
        {
            if (id.Contains(itemName))
            {
                hasItem = true;
                break;
            }
        }

        if (hasItem)
        {
            cogObject.SetActive(true);
            Debug.Log($"Success, {itemName} found in inventory.");
        }
        else
        {
            cogObject.SetActive(false);
            Debug.Log($"Fail, {itemName} found in inventory.");
        }
    }
}
