using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;

    [field: SerializeField]
    public bool MouseClickInput {private set; get;}
    public Action MouseOnClickInput;
    public Action MouseOnUpInput;
    public Action InvOnClick;
    public Action FlashInput;
    [field: SerializeField]
    public bool SkipInput {private set; get;}
    public Vector2 MouseInput;
    public bool HintInput;

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

        invAction.performed += _ => InvOnClick?.Invoke();

        flashAction.performed += _ => FlashInput?.Invoke();

        skipAction.performed += _ => SkipInput = true;
        skipAction.canceled += _ => SkipInput = false;

        mouseClickAction.performed += _ => MouseClickInput = true;
        mouseClickAction.canceled += _ => MouseClickInput = false;

        mouseClickAction.performed += _ => MouseOnClickInput?.Invoke();
        mouseClickAction.canceled += _ => MouseOnUpInput?.Invoke();

        hintAction.performed += _ => HintInput = true;
        hintAction.canceled += _ => HintInput= false;
    }

    void Update()
    {
        MouseInput = mouseAction.ReadValue<Vector2>();
    }
}
