using UnityEngine;

public class _Transition_Item : SpecialItems
{
    public string transition_to_;
    public override void EnterCondition()
    {
        Debug.Log(transition_to_);
        if (transition_to_ != "")
        {
            Debug.Log("Transitioning to " + transition_to_);
            SceneController.scene_controller_instance.PlayerShouldReturnTo(transform.position);
            FadeController.Instance.FadeAndLoad(transition_to_);
        }
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