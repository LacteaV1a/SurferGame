using UnityEngine;

[DefaultExecutionOrder(-1)]
public abstract class InputManager: Singleton<InputManager>
{
    protected PlayerControls _playerControls;

    public delegate void PlayerInputCallback(Vector2 direction);
    public PlayerInputCallback PlayerInputEvent;
    protected override void Awake()
    {
        base.Awake();
        _playerControls = new PlayerControls();
    }

    public void OnEnable()
    {
        _playerControls.Enable();
    }

    public void OnDisable()
    {
        _playerControls.Disable();
    }

}
