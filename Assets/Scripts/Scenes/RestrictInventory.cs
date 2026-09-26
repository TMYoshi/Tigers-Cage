using UnityEngine;

public class RestrictInventory : MonoBehaviour
{
    private Canvas inventoryCanvas;

    private void Start()
    {
        GameObject invObj = GameObject.Find("InventoryCanvas");
        if (invObj != null)
        {
            inventoryCanvas = invObj.GetComponent<Canvas>();
            if (inventoryCanvas != null) inventoryCanvas.enabled = false;
        }
    }

    private void OnDisable()
    {
        if (inventoryCanvas != null)
            inventoryCanvas.enabled = (true);
    }
}
