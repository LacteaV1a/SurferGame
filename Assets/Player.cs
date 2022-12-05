using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private ObstaclesDetector _obstaclesDetector;

    [SerializeField] private PlayerMovementAnimation _movementAnimation;
    [SerializeField] private ActionPlayerAnimation _actionAnimation;

    private PlayerHealth _health;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _health = new PlayerHealth(1);
    }

    private void OnEnable()
    {
        _obstaclesDetector.OnObstacleDetected += OnObstacleDetected;
    }

    private void OnObstacleDetected(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out IDamageDealer dd))
        {
            _health.TakeDamage(dd);

            if (_health.Value == 0)
            {
                _actionAnimation.Dead();
                _playerMovement.MoveSpeed = 0;
            }
        }
    }

    private void OnDisable()
    {
        _obstaclesDetector.OnObstacleDetected -= OnObstacleDetected;
    }
}
