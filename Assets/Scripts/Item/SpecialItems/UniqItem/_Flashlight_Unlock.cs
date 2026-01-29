using UnityEngine;

public class _Flashlight_Unlock : SpecialItems
{
	[SerializeField] FlashlightPickup pickupScript;

    public override void EnterCondition()
    {
		pickupScript.PickupFlashlight();
    }
    public override bool CompleteCondition() 
    {
        return true;
    }
    public override bool ExitCondition()
    {
        return true;
    }
}
