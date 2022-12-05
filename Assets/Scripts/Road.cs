using UnityEngine;

public class Road : MonoBehaviour
{
    private Vector3 _startPostion;
    private Vector3 _endPosition;
    private int _length;
    public Vector3 StartPosition => _startPostion;
    public Vector3 EndPosition => _endPosition;

    public void AddObstacles(ObstacleModel[] obstacles)
    {
        for (int i = 0; i < _length; i++)
        {
            var spawn = GetRandomBool();
        }
        foreach (var obstacle in obstacles)
        {
            obstacle.gameObject.transform.SetParent(this.transform);
            obstacle.gameObject.transform.localPosition = GetRandomPosition(_startPostion, _endPosition);
        }
    }

    private bool GetRandomBool()
    {
        return Random.value >= 0.5;
    }

    private Vector3 GetRandomPosition(Vector3 from, Vector3 to) 
    {
        return new Vector3(Random.Range(from.x, to.x), Random.Range(from.y, to.y), (int)Random.Range(from.z, to.z));
    }

    public void Initialize(Vector3 startPos, Vector3 endPos, int length)
    {
        _startPostion = startPos;
        _endPosition = endPos;
        _length = length;
    }
}
