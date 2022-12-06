using System.Collections;
using UnityEngine;

public class InputKeyboardManager : InputManager
{
    private void Start()
    {
        _playerControls.PC.Up.performed += UpStarted;

        _playerControls.PC.Down.performed += DownStarted;

        _playerControls.PC.Right.performed += RightStarted;

        _playerControls.PC.Left.performed += LeftStarted;
    }

    private void UpStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("123");
        PlayerInputEvent?.Invoke(Vector2.up);
    }

    private void DownStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        PlayerInputEvent?.Invoke(Vector2.down);
    }

    private void RightStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        PlayerInputEvent?.Invoke(Vector2.right);
    }

    private void LeftStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        PlayerInputEvent?.Invoke(Vector2.left);
    }
}