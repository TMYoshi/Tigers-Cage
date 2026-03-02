using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;

<<<<<<< Updated upstream
    public bool MouseClickInput;
    public bool InvInput;
    public bool FlashInput;
    public bool SkipInput;
    public bool HintInput;
=======
    [field: SerializeField]
    public bool MouseClickInput {private set; get;}
    public Action MouseOnClickInput;
    [field: SerializeField]
    public bool InvInput {private set; get;}
    public Action InvOnClick;
    public Action FlashInput;
    [field: SerializeField]
    public bool SkipInput {private set; get;}
>>>>>>> Stashed changes
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

        flashAction.performed += _ => FlashInput.Invoke();

        skipAction.performed += _ => SkipInput = true;
        skipAction.canceled += _ => SkipInput = false;

        mouseClickAction.performed += _ => MouseClickInput = true;
        mouseClickAction.canceled += _ => MouseClickInput = false;

<<<<<<< Updated upstream
        hintAction.performed += _ => HintInput = true;
        hintAction.canceled += _ => HintInput= false;
=======
        mouseClickAction.performed += _ => MouseOnClickInput.Invoke();
>>>>>>> Stashed changes
    }

    void Update()
    {
        MouseInput = mouseAction.ReadValue<Vector2>();
    }
}
