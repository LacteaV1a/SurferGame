using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;

    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    #endregion

    private Camera _mainCamera;

    private PlayerControls _playerControls;

    protected override void Awake()
    {
        base.Awake();
        _playerControls = new PlayerControls();
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void Start()
    {
        _playerControls.Touch.PrimaryContanct.started += StartTouchPrimary;
        _playerControls.Touch.PrimaryContanct.canceled += EndTouchPrimary;
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        OnStartTouch?.Invoke(_playerControls.Touch.PrimaryPosition.ReadValue<Vector2>(), (float)context.time);
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        OnEndTouch?.Invoke(_playerControls.Touch.PrimaryPosition.ReadValue<Vector2>(), (float)context.startTime);
    }

}
