using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestrictScene : MonoBehaviour
{
    private GameObject inventory;
    private FlashlightController flashlight;

    private void Start()
    {
        inventory = GameObject.Find("InventoryCanvas");
        if (inventory != null)
            inventory.SetActive(false);

        flashlight = Object.FindFirstObjectByType<FlashlightController>();
        if(flashlight != null)
        {
            flashlight.SetFlashlightState(false);
            flashlight.enabled = false;
        }
    }

    private void OnDestroy()
    {
        if (inventory != null)
            inventory.SetActive(true);

        if(flashlight != null)
        {
            flashlight.enabled = true;
        }
    }
}