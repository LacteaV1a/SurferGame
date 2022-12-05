using System.Collections;
using UnityEngine;


public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private float _minimumDistance = 0.2f;
    [SerializeField] private float _maximumTime = 1f;
    [SerializeField, Range(0,1)] private float _directionThreshold = 0.9f;

    private InputManager _inputManager;

    private Vector2 _startPostion;
    private float _startTime;

    private Vector2 _endPosition;
    private float _endTime;

    public delegate void Swipe(Vector2 direction);
    public event Swipe OnSwiped;


    private void Awake()
    {
        _inputManager = InputManager.Instance;
    }


    private void OnEnable()
    {
        _inputManager.OnStartTouch += SwipeStart;
        _inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        _inputManager.OnStartTouch -= SwipeStart;
        _inputManager.OnEndTouch -= SwipeEnd;
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
            OnSwiped?.Invoke(Vector2.up);
        }
        else if (Vector2.Dot(Vector2.down, direction) > _directionThreshold)
        {
            Debug.Log("Swipe Down");
            OnSwiped?.Invoke(Vector2.down);

        }
        else if (Vector2.Dot(Vector2.right, direction) > _directionThreshold)
        {
            Debug.Log("Swipe Right");
            OnSwiped?.Invoke(Vector2.right);

        }
        else if (Vector2.Dot(Vector2.left, direction) > _directionThreshold)
        {
            Debug.Log("Swipe Left");
            OnSwiped?.Invoke(Vector2.left);
        }
    }


}
