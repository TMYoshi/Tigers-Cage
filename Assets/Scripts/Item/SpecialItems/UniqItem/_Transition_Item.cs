using UnityEngine;

public class _Transition_Item : SpecialItems
{
    public string transition_to_;
    [SerializeField] AudioClip audio_clip_;
    bool already_played_ = false;

    public override void EnterCondition()
    {
        if(already_played_) return;

        if(audio_clip_ != null) SFXManager.Instance.PlaySFXClip(audio_clip_);
        already_played_ = true;

        Debug.Log(transition_to_);
        if (transition_to_ != "")
        {
            Debug.Log("Transitioning to " + transition_to_);
            SceneController.scene_controller_instance.PlayerShouldReturnTo(transform.position);
            SaveLoad.Instance.SaveGame();
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
