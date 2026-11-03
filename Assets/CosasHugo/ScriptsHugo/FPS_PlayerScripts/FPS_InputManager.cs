using UnityEngine;
using System;

public class FPS_InputManager : MonoBehaviour
{
    public static FPS_InputManager Instance;
    
    private FPSInputActions inputActions;

    public Action Jump;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        inputActions = new FPSInputActions();
        inputActions.Enable();
    }
    
    public Vector2 GetInputMoveValue()
    {
        return inputActions.Combat.Move.ReadValue<Vector2>();
    }
    
    public Vector2 GetInputLookValue()
    {
        return inputActions.Combat.Look.ReadValue<Vector2>();
    }
    
    private void OnJumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Jump?.Invoke();
    }
    
    private void OnEnable()
    {
        inputActions.Combat.Jump.performed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        inputActions.Combat.Jump.performed -= OnJumpPerformed;
    }
}
