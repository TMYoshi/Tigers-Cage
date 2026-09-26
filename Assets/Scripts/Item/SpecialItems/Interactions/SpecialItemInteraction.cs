using UnityEngine;

public class SpecialItemInteraction : Interaction
{
    public SpecialItems specialItems;
    [SerializeField] AudioClip interactionAudio;
    
    public override void ExecuteEffect(PlayerStateManager _context)
    {
        if(interactionAudio != null)
            SFXManager.Instance.PlaySFXClip(interactionAudio);
        _context._ItemManager._SelectedItem.AssignSpecialEvents(specialItems);
        _context.UpdateCurrentState(PlayerStateManager.State.SpecialItem);
    }
}

