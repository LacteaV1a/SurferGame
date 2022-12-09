using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _prefabTrack;
    [SerializeField] private float _lenghtTrack;
    [SerializeField] private int _countInitTrack;
    [SerializeField] private float _offsetDestroyTrack;
    [SerializeField] private Vector3 _startPos;
    [SerializeField] private Transform _player;
    [SerializeField] private LevelTrackConfiguration _config;

    private Track[] _initRoads;
    private int _trackCounter = 0;

    private Track _currentTrack;
    public Track CurrentTrack => _currentTrack;
    public Action<Track> OnCurrentTrackChanged;
    public Action<Track> OnTrackGenerated;

    private void Start()
    {
        _config.Inittialize();
        _lenghtTrack = _prefabTrack.GetComponent<Track>().Length;
        _initRoads = new Track[_countInitTrack];
    }

    private void Update()
    {
        //_initRoads[i].transform.position -= new Vector3(_startPos.x, _startPos.y, Time.deltaTime * _speed);
        var triggerPosZ = (_startPos.z + _lenghtTrack * (_trackCounter - _countInitTrack + 1)) + _offsetDestroyTrack;
        if (_player.position.z > triggerPosZ || _trackCounter < _countInitTrack)
        {

            CreateTrack(_prefabTrack.GetComponent<Track>());
        }

        var triggerPosZWithoutOffset = triggerPosZ - _offsetDestroyTrack;
        if(_player.position.z > triggerPosZWithoutOffset)
        {
            _currentTrack = _initRoads[0];
            OnCurrentTrackChanged?.Invoke(_currentTrack);
        }

    }


    public void CreateTrack(Track track)
    {
        var curPos = new Vector3(_startPos.x, _startPos.y, _startPos.z + (_trackCounter * _lenghtTrack));
        track = Instantiate(track, curPos, Quaternion.identity);

        track.name = track.name + _trackCounter;

        //var track = trackGO.GetComponent<Track>();
        track.Initialize();
        OnTrackGenerated?.Invoke(track);

        if (_trackCounter < _countInitTrack)
        {
            _initRoads[_trackCounter] = track;
        }
        else
        {
            Destroy(_initRoads[0].gameObject);
            ShiftLeft(_initRoads);
            _initRoads[^1] = track;

        }

        _trackCounter++;
    }

    private void ShiftLeft(Track[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
            array[i] = array[i + 1];
    }
}
