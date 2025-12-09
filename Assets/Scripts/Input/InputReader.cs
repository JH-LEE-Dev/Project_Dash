using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader
{
    public event Action<Vector2> RightClickEvent;
    public event Action<Vector2> LeftClickEvent;
    public event Action LeftClickReleased;

    private InputActionSystem actions;

    public void Initialize()
    {
        if (actions == null)
        {
            actions = new InputActionSystem();

            actions.Combat.LeftClick.performed += OnSelect;
            actions.Combat.LeftClick.canceled += OnLeftClickReleased;
        }

        actions.Combat.Enable();
    }

    public void Release()
    {
        actions.Combat.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        RightClickEvent?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnSelect(InputAction.CallbackContext context)
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();

        LeftClickEvent?.Invoke(screenPos);
    }

    private void OnLeftClickReleased(InputAction.CallbackContext context)
    {
        LeftClickReleased?.Invoke();
    }
}
