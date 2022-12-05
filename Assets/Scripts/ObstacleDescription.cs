using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ObstacleDescriptions", menuName = "ObstacleDescriptions")]
public class ObstacleDescription : ScriptableObject
{
    [SerializeField] private List<GameObject> _prefabs = new List<GameObject>();
    public List<GameObject> Prefabs => _prefabs;
}
