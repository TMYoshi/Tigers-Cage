using UnityEngine;

public class PlayerHidingState : PlayerBaseState
{
    private HidingSpot current_spot_;
    public HidingSpot GetHidingSpot() { return current_spot_; }
    private HeartbeatMinigame minigame_;

    public PlayerHidingState(PlayerStateManager context) : base(context)
    {
        _context = context;
    }
    public override void EnterState()
    {
        current_spot_ = (HidingSpot)Object.FindAnyObjectByType(typeof(HidingSpot));
    }
    public override void UpdateState()
    {
        if(Countdown.Instance.GetRemTime() == 0)
        {
            if(GetHidingSpot().IsValidSpot())
            {
                Debug.Log("Woohoo");
                minigame_ = (HeartbeatMinigame)Object.FindAnyObjectByType(typeof(HeartbeatMinigame));
                minigame_.StartHeartBeatMinigame();
            }

            else
            {
            // Failure cutscene
                Debug.Log("uh oh");
                SceneController.scene_controller_instance.FadeAndLoadScene("GameOver");
            }
            _context.UpdateCurrentState(PlayerStateManager.State.Idle);   
        }
    }
    public override void ExitState()
    {
    }
}
