using DG.Tweening;
using DG.Tweening.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _player;
    [SerializeField] private Collider _playerCollider;
    [Header("Jump Settings")]
    [SerializeField] private AnimationCurve _easyYHieght;
    [SerializeField] private float _jumpHieght;
    [SerializeField] private float _timeJump;
    [Header("Move Settings")]
    [SerializeField] private float _moveSpeed = 4.5f;
    [SerializeField] private float _moveHorizontalDistance = 1;

    [Header("Collision settings")]
    [SerializeField] private float _distanceRaycast = 0.1f;
    [SerializeField] private LayerMask _obstacleMask;

    private SwipeDetection _swipeDetection;
    private bool _isGrounded;

    private Vector2[] _sidesHorizont2D = new Vector2[] {Vector2.left, Vector2.right };
    private Vector3[] _sidesHorizont3D = new Vector3[] { Vector3.left, Vector3.right };


    private Tween _jumpTween;
    private Tween _moveTween;

    public event Action OnJumpedStart;
    public event Action OnJumpedEnd;

    public bool IsGrounded => _isGrounded;
    public float MoveSpeed { get => _moveSpeed; set { _moveSpeed = value; } }
    public bool CanMove { get; set; } = true;

    private void Awake()
    {
        _swipeDetection = FindObjectOfType<SwipeDetection>();
    }

    private void OnEnable()
    {
        _swipeDetection.OnSwiped += OnSwiped;
    }

    private void OnSwiped(Vector2 direction)
    {
        if(direction == Vector2.left || direction == Vector2.right)
        {
            MoveHorizontal(direction);
        }
        else if(direction == Vector2.up)
        {
            Jump();
        }

    }

    private void Update()
    {
        if (CanMove == false) return;
        MoveForaward();


        //for debug
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPosition();
        }

        _isGrounded = CheckIfGround();

        if (_isGrounded && _jumpTween != null && _jumpTween.position > 0.1f) _jumpTween.Kill();
    }
    
    private bool CheckIfGround()
    {
        RaycastHit hit;
        return Physics.Raycast(_playerCollider.bounds.center, Vector3.down, out hit, (_playerCollider.bounds.size.y / 2) + _distanceRaycast);
    }

    public void ResetPosition()
    {
        _player.transform.position = new Vector3(0, 0, _player.transform.position.z);
    }
    private void MoveForaward()
    {
        _player.transform.Translate(new Vector3(0, 0, Time.deltaTime * _moveSpeed));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_playerCollider.bounds.center, _playerCollider.bounds.center + Vector3.down * (_playerCollider.bounds.size.y / 2 + _distanceRaycast));

        for (int i = 0; i < _sidesHorizont3D.Length; i++)
        {
            var bounds = _playerCollider.bounds;
            var minCenter = new Vector3(bounds.center.x, bounds.min.y+0.1f, bounds.center.z);
            Gizmos.DrawLine(minCenter, minCenter + _sidesHorizont3D[i] * (_playerCollider.bounds.size.x / 2 + _moveHorizontalDistance));
        }

        // Gizmos.DrawWireSphere(new Vector3(_playerCollider.transform.position.x,
        //     0,
        //     _playerCollider.transform.position.z+((_timeJump - (_jumpTween != null ? _jumpTween.position : 0)) *  4.5f)), 0.5f);
    }

    private void Jump()
    {
        if (IsGrounded)
        {
            _jumpTween = _player.transform
                .DOMoveY(_player.transform.position.y + _jumpHieght, _timeJump)
                .SetEase(_easyYHieght);

            StartCoroutine(JumpEnd());

            OnJumpedStart?.Invoke();
        }
    }

    private IEnumerator JumpEnd()
    {
        yield return _jumpTween?.WaitForPosition(_timeJump - 0.1f);
        OnJumpedEnd?.Invoke();
    }

    public void SetMoveHorizontalDistance(float distance)
    {
        _moveHorizontalDistance = distance;
    }

    private void MoveHorizontal(Vector2 direction)
    {
        for (int i = 0; i < _sidesHorizont3D.Length; i++)
        {
            var bounds = _playerCollider.bounds;
            Ray ray = new Ray(new Vector3(bounds.center.x, bounds.min.y + 0.1f, bounds.center.z), _sidesHorizont3D[i]);
            if (Physics.Raycast(ray, (_playerCollider.bounds.size.x / 2) + _moveHorizontalDistance, _obstacleMask))
            {
                if (direction == _sidesHorizont2D[i])
                {
                    return;
                }
            }
        }


        _moveTween = _player.transform
            .DOMoveX(_player.transform.position.x + direction.x * _moveHorizontalDistance, 0.3f)
            .SetEase(Ease.InOutCirc);
    }

    private void OnDisable()
    {
        _swipeDetection.OnSwiped -= OnSwiped;
    }

}
