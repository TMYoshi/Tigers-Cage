using UnityEngine;

public class FlashlightPickup : MonoBehaviour
{
    public void PickupFlashlight()
    {
        FlashlightController flashlightController = FindFirstObjectByType<FlashlightController>();

        if (flashlightController != null)
        {
            flashlightController.UnlockFlashlight();
            Debug.Log("flashlight unlocked");

            Destroy(gameObject);
            // add ot journal here
        }

        else {
            Debug.LogError("FlashlightController not found!");
        }
    }
}
