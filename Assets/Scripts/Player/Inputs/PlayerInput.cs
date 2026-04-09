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
    //public Action MouseMovement;
    
    [field: SerializeField]
    public bool SkipInput {private set; get;}
    public Vector2 MouseInput {private set; get;}
    public Vector2 MouseMovement {private set; get;}
    public bool HintInput;  
    public bool JournalInput {private set; get;}

    InputAction mouseClickAction;
    InputAction mouseAction;
    InputAction invAction;
    InputAction flashAction;
    InputAction skipAction;
    InputAction hintAction;
    //InputAction journalAction;

    InputAction mouseMoveAction;

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
            return;
        }

        mouseAction = InputSystem.actions.FindAction("MouseLocation");
        invAction = InputSystem.actions.FindAction("Inventory");
        mouseClickAction = InputSystem.actions.FindAction("Interact");
        flashAction = InputSystem.actions.FindAction("Flashlight");
        skipAction = InputSystem.actions.FindAction("Skip");
        hintAction = InputSystem.actions.FindAction("Hint");
        // journalAction = InputSystem.actions.FindAction("Journal");
        mouseMoveAction = InputSystem.actions.FindAction("MouseMove");

        invAction.performed += _ => InvOnClick?.Invoke();

        flashAction.performed += _ => FlashInput?.Invoke();

        skipAction.performed += _ => SkipInput = true;
        skipAction.canceled += _ => SkipInput = false;

        mouseClickAction.performed += _ => {
            MouseClickInput = true;
            MouseOnClickInput?.Invoke();
        };

        mouseClickAction.canceled += _ => {
            MouseClickInput = false;
            MouseOnUpInput?.Invoke();
        };

        hintAction.performed += _ => HintInput = true;
        hintAction.canceled += _ => HintInput= false;

        //journalAction.performed += _ => JournalInput = true;
        //journalAction.canceled += _ => JournalInput= false;
    }

    void Update()
    {
        if(mouseAction != null)
        {
            MouseInput = mouseAction.ReadValue<Vector2>();
        }
        if(mouseMoveAction != null)
        {
            MouseMovement = mouseMoveAction.ReadValue<Vector2>();
        }
        else
        {
            Debug.Log("MouseMove action not found!");
        }
    }

}
