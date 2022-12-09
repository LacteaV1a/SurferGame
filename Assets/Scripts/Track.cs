using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public abstract class Track : MonoBehaviour, ITItemLevelTrackConfiguration
{
    [SerializeField] private int _roadCount;
    [SerializeField] private float _roadSpace;
    [SerializeField] private float _roadOffset;

    [SerializeField] private int _widthTrack;
    [SerializeField] private int _lengthTrack;
    [SerializeField] private Transform _endTrackPoint;
    private Road[] _roads;

    public Transform EndTrackPoint => _endTrackPoint;

    public int Width => _widthTrack;
    public int Length => _lengthTrack;
    public float RoadSpace => _roadSpace;

    public IEnumerable<Road> Roads => _roads;

    public int ID { get; set; } = 0;

    public virtual void Initialize()
    {
        _roads = new Road[_roadCount];

        for (int i = 0; i < _roadCount; i++)
        {
            var startPos = new Vector3(((i * _roadSpace) - _widthTrack / 2f) + _roadOffset, 0, 0);
            var endPos = new Vector3(startPos.x, 0, _lengthTrack);

            var road = new GameObject(i + " Road").AddComponent<Road>();
            road.transform.SetParent(transform);
            road.transform.localPosition = Vector3.zero;
            road.Initialize(startPos, endPos, _lengthTrack);

            _roads[i] = road;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for (int i = 0; i < _roadCount; i++)
        {
            var startPos = new Vector3(((i * _roadSpace) - _widthTrack / 2f) + _roadOffset, 0,0) + (_roads != null ? _roads[i].transform.position:Vector3.zero);
            var endPos = new Vector3(((i  * _roadSpace) - _widthTrack / 2f) + _roadOffset, 0,_lengthTrack) + (_roads != null ? _roads[i].transform.position : Vector3.zero);

            Handles.DrawBezier(startPos, endPos, startPos, endPos, Color.green, null, 4f);
        }
    }
#endif
}
