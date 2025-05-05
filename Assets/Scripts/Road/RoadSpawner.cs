using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(RoaderStorage), typeof(HandleRoadSpeed), typeof (CoinSpawner))]
public class RoadSpawner : ObjectPool<Roader>
{
    [SerializeField] private Roader[] _roadPrefabs;
    [SerializeField] private Character _character;
    [SerializeField] private ClosestRoadTracker _roadTracker;

    [SerializeField] private int _maxRoads;
    [SerializeField] private float _spawnTriggerDistance;

    private RoaderStorage _storage;
    private Transform _transform;
    private HandleRoadSpeed _roadMovement;
    //private ScoreCounter _scoreCounter;
    private CoinSpawner _coinSpawner;

    private bool _hasSpawnedNextRoad;

    private void Awake()
    {
        _transform = transform;
        _coinSpawner = GetComponent<CoinSpawner>();
        _roadMovement = GetComponent<HandleRoadSpeed>();
        _storage = GetComponent<RoaderStorage>();
    }

    private void Start() =>
         Spawn();

    private void Update()
    {
        if (_storage.ActiveRoads.Count == 0)
            return;

        Roader lastRoad = _storage.ActiveRoads[^1];

        if (_character.transform.position.z > lastRoad.End.position.z + _spawnTriggerDistance && !_hasSpawnedNextRoad)
        {
            Spawn();
            _hasSpawnedNextRoad = true;
        }
        else if (_character.transform.position.z < lastRoad.End.position.z + _spawnTriggerDistance + _spawnTriggerDistance)
        {
            _hasSpawnedNextRoad = false;
        }

        if (_storage.ActiveRoads.Count >= _maxRoads)
            ReturnRoad();
    }

    //public void Initialize(ScoreCounter scoreCounter) =>
    //    _scoreCounter = scoreCounter;

    private void Spawn()
    {
        Roader randomPrefab = GetRandomRoad();
        Roader newRoad = GetObject(randomPrefab);

        if (newRoad == null)
            return;

        Vector3 spawnPosition = CalculateRoadPosition(newRoad);
        newRoad.transform.position = spawnPosition;
        //newRoad.Initialize(_scoreCounter);
        _storage.AddRoad(newRoad);
        _roadMovement.SetCurrentSpeed(newRoad);
        _coinSpawner.Generate(newRoad, newRoad.CoinSpawnPoints);
    }

    private Roader GetRandomRoad()
    {
        List<float> chances = new List<float>();

        for (int i = 0; i < _roadPrefabs.Length; i++)
            chances.Add(_roadPrefabs[i].ChanceFromDistance.Evaluate(_character.transform.position.z));

        float randomIndex = Random.Range(0, chances.Sum());
        float sum = 0;

        for (int i = 0; i < chances.Count; i++)
        {
            sum += chances[i];

            if (randomIndex < sum)
                return _roadPrefabs[i];
        }

        return _roadPrefabs[_roadPrefabs.Length - 1];
    }

    private Vector3 CalculateRoadPosition(Roader newRoad)
    {
        if (_storage.ActiveRoads.Count == 0)
            return _transform.position;

        Roader lastRoad = _storage.ActiveRoads[^1];
        return lastRoad.End.position - (newRoad.Begin.position - newRoad.transform.position);
    }

    private void ReturnRoad()
    {
        if (_storage.ActiveRoads.Count == 0)
            return;

        Roader firstRoad = _storage.ActiveRoads[0];
        Roader currentRoad = _roadTracker.ClosestRoad;

        if (firstRoad == currentRoad)
            return;

        _storage.RemoveRoad(firstRoad);
        firstRoad.Reset();
        ReturnObject(firstRoad);
    }
}