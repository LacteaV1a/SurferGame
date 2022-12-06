using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTouchManager : InputManager
{
    [SerializeField] private float _minimumDistance = 0.2f;
    [SerializeField] private float _maximumTime = 1f;
    [SerializeField, Range(0,1)] private float _directionThreshold = 0.9f;

    private Vector2 _startPostion;
    private float _startTime;

    private Vector2 _endPosition;
    private float _endTime;

    private void Start()
    {
        _playerControls.Touch.PrimaryContanct.started += StartTouchPrimary;
        _playerControls.Touch.PrimaryContanct.canceled += EndTouchPrimary;
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        SwipeStart(_playerControls.Touch.PrimaryPosition.ReadValue<Vector2>(), (float)context.time);
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        SwipeEnd(_playerControls.Touch.PrimaryPosition.ReadValue<Vector2>(), (float)context.startTime);
    }

    private void SwipeStart(Vector2 position, float time)
    {
        _startPostion = position;
        _startTime = time;
    }
     
    private void SwipeEnd(Vector2 position, float time)
    {
        _endPosition = position;
        _endTime = time;

        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if(Vector3.Distance(_startPostion, _endPosition) >= _minimumDistance &&
            (_endTime - _startTime) <= _maximumTime)
        {
            Debug.Log("Swipe Detected");

            Vector2 direction = _endPosition - _startPostion;

            SwipeDirection(direction.normalized);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if(Vector2.Dot(Vector2.up, direction) > _directionThreshold)
        {
            Debug.Log("Swipe Up");
            PlayerInputEvent?.Invoke(Vector2.up);
        }
        else if (Vector2.Dot(Vector2.down, direction) > _directionThreshold)
        {
            Debug.Log("Swipe Down");
            PlayerInputEvent?.Invoke(Vector2.down);

        }
        else if (Vector2.Dot(Vector2.right, direction) > _directionThreshold)
        {
            Debug.Log("Swipe Right");
            PlayerInputEvent?.Invoke(Vector2.right);

        }
        else if (Vector2.Dot(Vector2.left, direction) > _directionThreshold)
        {
            Debug.Log("Swipe Left");
            PlayerInputEvent?.Invoke(Vector2.left);
        }
    }


}
