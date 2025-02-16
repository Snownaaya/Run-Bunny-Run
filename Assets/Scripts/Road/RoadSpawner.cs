using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RoaderStorage), typeof(HandleRoadSpeed))]
public class RoadSpawner : ObjectPool<Roader>
{
    [SerializeField] private Roader[] _roadPrefabs;

    [SerializeField] private int _maxRoads = 8;

    private RoaderStorage _storage;
    private Vector3 _beginOffset;
    private Transform _transform;
    private HandleRoadSpeed _roadMovement;

    private float _spawnInterval = 18f;

    private void Awake()
    {
        _transform = transform;
        _roadMovement = GetComponent<HandleRoadSpeed>();
        _storage = GetComponent<RoaderStorage>();
    }

    private void Start()
    {
        StartCoroutine(GeneratorRoad());
        Spawn();
    }

    private IEnumerator GeneratorRoad()
    {
        var waitForSecond = new WaitForSeconds(_spawnInterval);

        while (enabled)
        {
            Spawn();
            yield return waitForSecond;
        }
    }

    private void Spawn()
    {
        if (_storage.ActiveRoads.Count >= _maxRoads)
            ReturnRoad();

        Roader randomPrefab = GetRandomRoad(); 
        Roader newRoader = GetObject(randomPrefab); 

        if (newRoader == null)
            return;

        Vector3 spawnPosition = CalculateRoadPosition(newRoader);
        newRoader.transform.position = spawnPosition;
        _storage.AddRoad(newRoader);
        _roadMovement.SetCurrentSpeed(newRoader);
    }

    private Roader GetRandomRoad()
    {
        int randomIndex = Random.Range(0, _roadPrefabs.Length);
        return _roadPrefabs[randomIndex];
    }

    private Vector3 CalculateRoadPosition(Roader newRoader)
    {
        if (_storage.ActiveRoads.Count == 0)
            return _transform.position;

        Roader lastRoader = _storage.ActiveRoads[^1];
        return lastRoader.End.position - (newRoader.Begin.position - newRoader.transform.position);
    }

    public void ReturnRoad()
    {
        Roader firstRoader = _storage.ActiveRoads[0];
        _storage.RemoveRoad(firstRoader);
        ReturnObject(firstRoader);
    }
}