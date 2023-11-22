using System;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class ArcadePlayerInputs : MonoBehaviour, ArcadePlayerInputActions.IPlayerActions
{
    private ArcadePlayerInputActions _input;
    
    [Header("Character Input Values")]
    public Camera Camera;
    public bool IsFire;
    public Vector2 ScreenPostion = Vector2.zero;

#if UNITY_STANDALONE
    [Header("Mouse Cursor Settings")]
    public bool CursorLocked = true;
    public bool CursorInputForLook = true;
#endif

    private void Awake()
    {
        _input = new ArcadePlayerInputActions();
        _input.Player.SetCallbacks(this);
        Camera = Camera == null ? Camera.main : Camera;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

#if UNITY_STANDALONE
    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(CursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
#endif

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            IsFire = true;

            OnFired?.Invoke();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            IsFire = false;
        }
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ScreenPostion = context.ReadValue<Vector2>();
        }
    }

    public event Action OnFired;
}
