using UnityEngine;

public class FlashlightPickup : SpecialItems
{
    public override void EnterCondition() { }
    public override bool CompleteCondition() => true;
    public override bool ExitCondition() => true;
    public override void RewardCondition() => base.RewardCondition();

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;
        UnlockFlashlight();
    }

    private void UnlockFlashlight()
    {
        PlayerPrefs.SetInt("FlashlightUnlocked", 1);
        PlayerPrefs.Save();
        Debug.Log("Flashlight perma unlocked");

        FlashlightController flashlightController = FindAnyObjectByType<FlashlightController>();
        if (flashlightController != null)
        {
            flashlightController.isFlashlightUnlocked = true;
            Debug.Log("Flashlight perma unlocked");
        }
    }
}