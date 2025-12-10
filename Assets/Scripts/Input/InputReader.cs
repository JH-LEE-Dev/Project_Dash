using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader
{
    public event Action<Vector2> LeftClickEvent;
    public event Action LeftClickReleasedEvent;
    public event Action<Vector2> SpawnButtonPressedEvent;
    public event Action SwitchPrefabButtonPressedEvent;

    private InputActionSystem actions;

    public void Initialize()
    {
        if (actions == null)
        {
            actions = new InputActionSystem();

            actions.Combat.LeftClick.performed += OnSelect;
            actions.Combat.LeftClick.canceled += OnLeftClickReleased;
            actions.Combat.ToSpawnMode.performed += ToSpawnModeKeyPressed;

            actions.System.ToCombatMode.performed += ToCombatModeKeyPressed;
            actions.System.SpawnButton.performed += SpawnButtonPressed;
            actions.System.SwitchPrefab.performed += SwitchPrefabButtonPressed;
        }

        actions.Combat.Enable();
        actions.System.Disable();
    }

    public void Release()
    {
        actions.Combat.Disable();
    }

    private void OnSelect(InputAction.CallbackContext context)
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();

        LeftClickEvent?.Invoke(screenPos);
    }

    private void OnLeftClickReleased(InputAction.CallbackContext context)
    {
        LeftClickReleasedEvent?.Invoke();
    }

    private void SpawnButtonPressed(InputAction.CallbackContext context)
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();

        SpawnButtonPressedEvent?.Invoke(screenPos);
    }

    private void ToSpawnModeKeyPressed(InputAction.CallbackContext context)
    {
        actions.Combat.Disable();
        actions.System.Enable();
    }

    private void ToCombatModeKeyPressed(InputAction.CallbackContext context)
    {
        actions.Combat.Enable();
        actions.System.Disable();
    }

    private void SwitchPrefabButtonPressed(InputAction.CallbackContext context)
    {
        SwitchPrefabButtonPressedEvent?.Invoke();
    }
}
