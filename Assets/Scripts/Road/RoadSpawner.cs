using System.Collections;
using UnityEngine;

public class RoadSpawner : ObjectPool<Roader>
{
    [SerializeField] private Roader _roader;

    [SerializeField] private int _maxRoads = 10;
    [SerializeField] private float _spawnOffsetZ = 20f;

    private RoaderStorage _storage;

    private float _spawnInterval = 10f;

    private void Awake()
    {
        _storage = GetComponent<RoaderStorage>();
        _storage.AddRoad(_roader);
    }

    private void Start() =>
        StartCoroutine(GeneratorRoad());

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
        if (_storage.ActiveRoads.Count > _maxRoads)
            ReturnRoad();

        Roader randomRoad = GetRandomRoad();
        Roader newRoader = GetObject(randomRoad);

        if (newRoader == null)
            return;

        Vector3 spawnPosition = CalculateRoadPosition(newRoader);

        newRoader.transform.position = spawnPosition;
        _storage.AddRoad(newRoader);
    }

    private Roader GetRandomRoad()
    {
        int randomIndex = Random.Range(0, _storage.ActiveRoads.Count);
        return _storage.ActiveRoads[randomIndex];
    }

    private Vector3 CalculateRoadPosition(Roader newRoader)
    {
        if (_storage.ActiveRoads.Count == 1)
            return Vector3.zero;

        Roader lastRoader = _storage.ActiveRoads[_storage.ActiveRoads.Count - 1];

        Vector3 direction = lastRoader.End.position - newRoader.Begin.localPosition;

        return direction;
    }

    public void ReturnRoad()
    {
        Roader firstRoader = _storage.ActiveRoads[1];

        _storage.RemoveRoad(firstRoader);
        ReturnObject(firstRoader);
    }
}