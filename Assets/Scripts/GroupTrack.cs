using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GroupTrack : ITItemLevelTrackConfiguration
{
    [SerializeField] private List<int> _tracksIds = new List<int>();

    public IEnumerable<int> TrackIds => _tracksIds;
    public int ID { get; set; }

    public void Add(int id)
    {
        _tracksIds.Add(id);
    }

    public void Remove(int id)
    {
        _tracksIds.Remove(id);
    }
}
