using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;

    public bool MouseClickInput;
    public bool InvInput;
    public Vector2 MouseInput;

    InputAction mouseClickAction;
    InputAction mouseAction;
    InputAction invAction;

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

        invAction.performed += _ => InvInput = true;
        invAction.canceled += _ => InvInput = false;

        mouseClickAction.performed += _ => MouseClickInput = true;
        mouseClickAction.canceled += _ => MouseClickInput = false;
    }

    void Update()
    {
        MouseInput = mouseAction.ReadValue<Vector2>();
    }
}
