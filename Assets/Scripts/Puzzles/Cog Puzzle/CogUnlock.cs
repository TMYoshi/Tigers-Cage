using UnityEngine;
using System.Collections;

public class CogUnlock : MonoBehaviour
{
    [SerializeField] private GameObject mediumLockedCog;
    [SerializeField] private GameObject largeLockedCog;

    private static bool mediumPreviouslyUnlocked = false;
    private static bool largePreviouslyUnlocked = false;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("CogUnlock starting inventory check");



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
        InventoryManager invManager = Object.FindFirstObjectByType<InventoryManager>();
        if (invManager == null) return false;

        bool foundInInventory = false;

        foreach(ItemSlot slot in invManager.itemSlot)
        {

            if (slot.isFull)
            {
                Debug.Log($"Checking slot: '{slot.itemName}' against target: '{itemName}'");
            }

            if (slot.isFull && slot.itemName.Replace(" ", "").ToLower().Contains(itemName.Replace(" ", "").ToLower()))
            {
                slot.RemoveItem();
                return true;
            }
        }

        if (foundInInventory)
        {
            // can save item data here if you want
        }
        return false;
    }
}
