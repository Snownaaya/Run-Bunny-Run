using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RoaderStorage), typeof(HandleRoadSpeed))]
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
    private bool _hasSpawnedNextRoad;

    private void Awake()
    {
        _transform = transform;
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


    private void Spawn()
    {
        Roader randomPrefab = GetRandomRoad();
        Roader newRoad = GetObject(randomPrefab);

        if (newRoad == null)
            return;

        Vector3 spawnPosition = CalculateRoadPosition(newRoad);
        newRoad.transform.position = spawnPosition;
        _storage.AddRoad(newRoad);
        _roadMovement.SetCurrentSpeed(newRoad);
    }

    private Roader GetRandomRoad()
    {
        int randomIndex = Random.Range(0, _roadPrefabs.Length);
        return _roadPrefabs[randomIndex];
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
        ReturnObject(firstRoad);
    }

    private void OnDrawGizmosSelected()
    {
        if (_storage == null || _storage.ActiveRoads.Count == 0)
            return;

        Roader lastRoad = _storage.ActiveRoads[^1];
        float triggerZ = lastRoad.End.position.z - _spawnTriggerDistance;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(-100, 0, triggerZ), new Vector3(100, 0, triggerZ));
    }
}