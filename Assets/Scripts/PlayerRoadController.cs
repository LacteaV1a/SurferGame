using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoadController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private TrackGenerator _trackGenerator;

    private void OnEnable()
    {
        _trackGenerator.OnCurrentTrackChanged += TrackChanged;
    }

    private void TrackChanged(Track track)
    {
        _playerMovement.SetMoveHorizontalDistance(track.RoadSpace);
    }


    private void OnDisable()
    {
        _trackGenerator.OnCurrentTrackChanged -= TrackChanged;
    }
}
