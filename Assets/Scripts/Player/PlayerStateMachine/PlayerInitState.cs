using UnityEngine;

public class PlayerInitState : MonoBehaviour
{
    public void PlayerOnStart()
    {
        if(SaveData.Instance.CollectedItemIds.Contains("MC Room - 3 South Wall_Rabbit_"))
        {
            PlayerStateManager.Instance.UpdateCurrentState(PlayerStateManager.State.Idle);
            PlayerAnimator.Instance.SetBunnyTrue();
        }
        else
        {
            PlayerStateManager.Instance.UpdateCurrentState(PlayerStateManager.State.Nervous);
        }
    }
}
