using UnityEngine;

public abstract class SpecialItems : MonoBehaviour
{
    [Header("Inventory Item (MUST BE ASSIGNED)")]
    public InventoryItem item;
    public void Start()
    {
        item.AssignSpecialEvents(this);
    }
    public abstract void EnterCondition();
    public abstract bool CompleteCondition();
    public abstract bool ExitCondition();
    public virtual void RewardCondition()
    {
        Debug.Log("Reward!!");
    }
    public virtual void CleanUpCondition()
    {

    }
}
