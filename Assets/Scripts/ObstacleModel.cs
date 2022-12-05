using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleModel : MonoBehaviour, IDamageDealer
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private int _length = 1;

    public int Length => _length;
    public int GetDamage()
    {
        return _damage;
    }
}
