using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TrackConfiguration", menuName = "TrackConfiguration")]
public class LevelTrackConfiguration : ScriptableObject
{
    [SerializeField] private List<Track> _tracks;
    [SerializeField] private List<GroupTrack> _groupsTrack;

    public Dictionary<int,Track> Tracks;
    public Dictionary<int, GroupTrack> GroupsTrack;


    [TextArea(5,10)]
    [SerializeField] string _itemOrder;
    public string ItemOrder => _itemOrder;

    public void Inittialize()
    {
        for (int i = 0; i < _tracks.Count; i++)
        {
            _tracks[i].ID = i;
            Tracks.Add(i, _tracks[i]);
        }

        for (int i = 0; i < _groupsTrack.Count; i++)
        {
            _groupsTrack[i].ID = i;
            GroupsTrack.Add(i, _groupsTrack[i]);
        }
    }
}