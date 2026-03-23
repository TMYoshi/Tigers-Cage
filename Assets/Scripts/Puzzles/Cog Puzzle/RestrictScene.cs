using UnityEngine;

public class RestrictScene : MonoBehaviour
{
    private Canvas inventoryCanvas;
    private FlashlightController flashlight;

    private void Start()
    {
        GameObject invObj = GameObject.Find("InventoryCanvas");
        if (invObj != null)
        {
            inventoryCanvas = invObj.GetComponent<Canvas>();
            if(inventoryCanvas != null ) inventoryCanvas.enabled = false;
        }

        flashlight = FindFirstObjectByType<FlashlightController>();
        if(flashlight != null)
        {
            flashlight.SetFlashlightState(false);
            flashlight.enabled = false;
        }
    }

    private void OnDisable()
    {
        if (inventoryCanvas != null)
            inventoryCanvas.enabled = (true);

        if(flashlight != null)
        {
            flashlight.enabled = true;
        }
    }
}