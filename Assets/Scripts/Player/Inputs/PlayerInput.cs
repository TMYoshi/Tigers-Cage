using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;

    public bool MouseClickInput;
    public bool InvInput;
    public bool FlashInput;
    public bool SkipInput;
    public bool HintInput;
    public Vector2 MouseInput;

    InputAction mouseClickAction;
    InputAction mouseAction;
    InputAction invAction;
    InputAction flashAction;
    InputAction skipAction;
    InputAction hintAction;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        mouseAction = InputSystem.actions.FindAction("MouseLocation");
        invAction = InputSystem.actions.FindAction("Inventory");
        mouseClickAction = InputSystem.actions.FindAction("Interact");
        flashAction = InputSystem.actions.FindAction("Flashlight");
        skipAction = InputSystem.actions.FindAction("Skip");
        hintAction = InputSystem.actions.FindAction("Hint");

        invAction.performed += _ => InvInput = true;
        invAction.canceled += _ => InvInput = false;

        flashAction.performed += _ => FlashInput = true;
        flashAction.canceled += _ => FlashInput = false;

        skipAction.performed += _ => SkipInput = true;
        skipAction.canceled += _ => SkipInput = false;

        mouseClickAction.performed += _ => MouseClickInput = true;
        mouseClickAction.canceled += _ => MouseClickInput = false;

        hintAction.performed += _ => HintInput = true;
        hintAction.canceled += _ => HintInput= false;
    }

    void Update()
    {
        MouseInput = mouseAction.ReadValue<Vector2>();
    }
}
