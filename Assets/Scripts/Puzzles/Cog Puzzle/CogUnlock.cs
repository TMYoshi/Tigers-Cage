using UnityEngine;

public class CogUnlock : MonoBehaviour
{
    [SerializeField] private GameObject mediumLockedCog;
    [SerializeField] private GameObject largeLockedCog;

    private static bool mediumPreviouslyUnlocked = false;
    private static bool largePreviouslyUnlocked = false;

    private void Start()
    {
        if (mediumPreviouslyUnlocked)
        {
            mediumLockedCog.SetActive(true);
        }
        else if (CheckAndUnlock("Medium Cog"))
        {
            mediumLockedCog.SetActive(true);
            mediumPreviouslyUnlocked = true;
        }

        if (largePreviouslyUnlocked)
        {
            largeLockedCog.SetActive(true);
        }
        else if (CheckAndUnlock("Large Cog"))
        {
            largeLockedCog.SetActive(true);
            largePreviouslyUnlocked = true;
        }
    }

    private bool CheckAndUnlock(string itemName)
    {
        string foundId = "";

        foreach (string id in InventoryManager.collectedItems)
        {
            if (id.Contains(itemName))
            {
                foundId = id;
                break;
            }
        }

        if (string.IsNullOrEmpty(foundId)) return false;

        InventoryManager.collectedItems.Remove(foundId);
        InventoryManager invManager = Object.FindFirstObjectByType<InventoryManager>();
        if (invManager != null)
        {
            foreach (ItemSlot slot in invManager.itemSlot)
            {
                if (slot.isFull && slot.itemName.Contains(itemName))
                {
                    slot.RemoveItem();
                    break;
                }
            }
        }
        return true;
    }
}
