using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OrderProcessing
{
    public bool IsFinished { get; private set; }
    private int[] _idsInProcess;
    private int[][] _repeats;
    private int _repeatCounter = 0;
    private int _idCounter = 0;
    public OrderProcessing(int id, int repeat = 1)
    {
        _idsInProcess = new int[1] { id };

        _repeats = new int[repeat][];
        for (int i = 0; i < repeat; i++)
        {
            _repeats[i] = _idsInProcess;
        }
    }

    public OrderProcessing(int[] ids, int repeat = 1)
    {
        _idsInProcess = ids;

        _repeats = new int[repeat][];
        for (int i = 0; i < repeat; i++)
        {
            _repeats[i] = _idsInProcess;
        }
    }

    public int GetId()
    {
        var id = -1;

        if (_idCounter < _idsInProcess.Length)
        {
            id = _repeats[_repeatCounter][_idCounter];

            _idCounter++;
        }

        if (_idCounter == _idsInProcess.Length)
        {
            _repeatCounter++;
            if (_repeatCounter == _repeats.Length)
            {
                IsFinished = true;
            }
            _idCounter = 0;
            id = _repeats[_repeatCounter][_idCounter];
        }

        return id;
    }

}
public class ItemTrackOrderParser
{
    private LevelTrackConfiguration _levelTrackConfig;

    private List<Track> _tracks = new List<Track>();
    public ItemTrackOrderParser(LevelTrackConfiguration levelTrackConfig)
    {
        _levelTrackConfig = levelTrackConfig;
        Initialize();
    }

    public void Initialize()
    {
        var tracsId = _levelTrackConfig.ItemOrder;
        var splitTracsId = tracsId.Split(" ");
        for (int i = 0; i < splitTracsId.Length; i++)
        {
            if (splitTracsId[i].All(char.IsDigit))
            {
                _tracks.Add(_levelTrackConfig.Tracks[int.Parse(splitTracsId[i])]);
            }

            if (splitTracsId[i][0] == 'g')
            {
                var groupTracsId = splitTracsId[i][1..];
                // _tracks.AddRange(_levelTrackConfig.GroupsTrack[int.Parse(groupTracsId)].TracsIDs);
            }
        }
    }

    public Track GetTrack()
    {
        return null;
    }
}
