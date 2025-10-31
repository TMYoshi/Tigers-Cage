using UnityEngine;

public class SpecialItemInteraction : Interaction
{
    public SpecialItems specialItems;
    
    public override void ExecuteEffect(PlayerStateManager _context)
    {
        _context._ItemManager._SelectedItem.AssignSpecialEvents(specialItems);
        _context.UpdateCurrentState(PlayerStateManager.State.SpecialItem);
    }
}

