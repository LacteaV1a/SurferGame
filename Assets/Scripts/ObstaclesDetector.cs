using System;
using UnityEngine;

public class ObstaclesDetector : MonoBehaviour
{
    [SerializeField] private Collider _playerCollider;
    [SerializeField] private LayerMask _layerObstacleMask;

    public event Action<Collision> OnObstacleDetected;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == Mathf.Log(_layerObstacleMask.value, 2))
        {
            //Debug.Log(Vector3.Dot(collision.contacts[0].normal, Vector3.up));
            if (Vector3.Dot(collision.contacts[0].normal, Vector3.forward) < -0.9f)
            {
                Debug.Log("collission detect");
                OnObstacleDetected?.Invoke(collision);
            }
        }
    }
}
