using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableInventory : MonoBehaviour
{
    private GameObject inventory;

    private void Start()
    {
        inventory = GameObject.Find("InventoryCanvas");
        if (inventory != null)
            inventory.SetActive(false);
    }

    private void OnDestroy()
    {
        if (inventory != null)
            inventory.SetActive(true);
    }
}