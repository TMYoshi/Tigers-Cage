using UnityEngine;

public class RestrictFlashlight : MonoBehaviour
{
    private FlashlightController flashlight;

    private void Start()
    {  
        flashlight = FindFirstObjectByType<FlashlightController>();
        if (flashlight != null)
        {
            flashlight.SetFlashlightState(false);
            flashlight.enabled = false;
        }
    }

    private void OnDisable()
    {
        if (flashlight != null)
        {
            flashlight.enabled = true;
        }
    }
}
