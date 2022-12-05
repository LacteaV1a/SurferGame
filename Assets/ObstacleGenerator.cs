using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private TrackGenerator _trackGenerator;
    [SerializeField] private ObstacleDescription _config;

    private void OnEnable()
    {
        _trackGenerator.OnTrackGenerated += OnTrackGenerated;
    }

    private void OnTrackGenerated(Track track)
    {

        foreach (var road in track.Roads)
        {
            var countItemOnRoad = Random.Range(0, 5);
            var obstacles = new ObstacleModel[countItemOnRoad];
            for (int i = 0; i < countItemOnRoad; i++)
            {
                obstacles[i] = Instantiate(_config.Prefabs[Random.Range(0, _config.Prefabs.Count)]).GetComponent<ObstacleModel>();
            }
            road.AddObstacles(obstacles);
        }
    }

    private void OnDisable()
    {
        _trackGenerator.OnTrackGenerated -= OnTrackGenerated;
    }
}
