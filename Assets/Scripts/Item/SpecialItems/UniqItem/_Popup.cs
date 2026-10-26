using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine;

public class _Popup: SpecialItems
{
    public UnityEvent OnExitItem;
    public override void EnterCondition()
    {
    }

    public override bool CompleteCondition() 
    {
        return true;
    }

    public override bool ExitCondition()
    {
        return true;
    }

    public override PlayerStateManager.State DialogExitCondition()
    {
        OnExitItem.Invoke();
        return PlayerStateManager.State.Idle;
    }

    public void JournalPopup()
    {
        InputAction saved_binding = InputSystem.actions.FindAction("Inventory");
        string binding_string = saved_binding.GetBindingDisplayString();

        if(!string.IsNullOrEmpty(binding_string))
            PopupManager.Instance.SetPopupOnMessage
                ("Press " + binding_string + " to use Journal", ref PlayerInput.Instance.InvOnClick);
        else
            PopupManager.Instance.SetPopupOnMessage
                ("Press E to use Journal", ref PlayerInput.Instance.InvOnClick);
    }

    public void FlashlightPopup()
    {
        InputAction saved_binding = InputSystem.actions.FindAction("Flashlight");
        string binding_string = saved_binding.GetBindingDisplayString();

        if(!string.IsNullOrEmpty(binding_string))
            PopupManager.Instance.SetPopupOnMessage
                ("Press " + binding_string + " to use Flashlight", ref PlayerInput.Instance.FlashOnClick);
        if(!string.IsNullOrEmpty(binding_string))
            PopupManager.Instance.SetPopupOnMessage
                ("Press F to use Flashlight", ref PlayerInput.Instance.FlashOnClick);
    }
}
