using UnityEngine;

public abstract class SpecialItems : MonoBehaviour
{
    [Header("Inventory Item (MUST BE ASSIGNED)")]
    public InventoryItem item;

    public virtual void Start()
    {
        item.AssignSpecialEvents(this);
    }
    public abstract void EnterCondition();
    public abstract bool CompleteCondition();
    public abstract bool ExitCondition();

    //when you want a specific event to happen on or before playing a dialog
    public virtual void DialogEnterCondition()
    {
        Debug.Log("Missing dialog enter condition on special item");
    }

    //when you want a specific event to happen on or before playing a dialog
    //also returns next state after dialog ends
    public virtual PlayerStateManager.State DialogExitCondition()
    {
        Debug.Log("Missing dialog exit condition on special item");
        return PlayerStateManager.State.Idle;
    }


    public virtual void RewardCondition()
    {
        
        Debug.Log("Reward!!");
    }
    public virtual void CleanUpCondition()
    {

    }
}
