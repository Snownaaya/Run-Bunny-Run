using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : ObjectPool<Obstacle>
{
    [SerializeField] private Obstacle _obstaclePrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnDelay = 1f;
    [SerializeField] private float _returnInterval = 25f;

    private readonly List<Obstacle> _activeObstacles = new List<Obstacle>();
    private WaitForSeconds _spawnWait;
    private WaitForSeconds _returnWait;

    private void Awake()
    {
        _spawnWait = new WaitForSeconds(_spawnDelay);
        _returnWait = new WaitForSeconds(_returnInterval);
    }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(ReturnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (enabled)
        {
            SpawnObstacle();
            yield return _spawnWait;
        }
    }

    private IEnumerator ReturnRoutine()
    {
        while (enabled)
        {
            if (_activeObstacles.Count > 0)
            {
                ReturnObstacle(_activeObstacles[0]);
                Debug.Log("Возвращён объект");
            }
            yield return _returnWait;
        }
    }

    public void SpawnObstacle()
    {
        Obstacle obstacle = GetObject(_obstaclePrefab);
        int spawnIndex = (_spawnPoints.Length > 0) ? Random.Range(0, _spawnPoints.Length) : 0;
        obstacle.transform.position = _spawnPoints[spawnIndex].position;
        _activeObstacles.Add(obstacle);
    }

    public void ReturnObstacle(Obstacle obstacle)
    {
        if (_activeObstacles.Remove(obstacle))
            ReturnObject(obstacle);
    }
}
