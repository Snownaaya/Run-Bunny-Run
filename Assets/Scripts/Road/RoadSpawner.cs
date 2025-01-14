using System.Collections;
using UnityEngine;

public class RoadSpawner : ObjectPool<Roader>
{
    [SerializeField] private Roader _roader;

    private RoaderStorage _storage;

    private float _spawnInterval = 15f;
    private float _checkInterval = 15f;

    private void Awake()
    {
        _storage = GetComponent<RoaderStorage>();
        _storage.AddRoad(_roader);
    }

    private void Start()
    {
        StartCoroutine(GeneratorRoad());
    }

    private IEnumerator GeneratorRoad()
    {
        var waitForSecond = new WaitForSeconds(_spawnInterval);

        while (enabled)
        {
            if (_storage.ActiveRoads.Count > 0)
                Spawn();

            yield return waitForSecond;
        }
    }

    private void Spawn()
    {
        Roader randomRoad = GetRandomRoad();
        Roader newRoader = GetObject(randomRoad);

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
        if (_storage.ActiveRoads.Count == 0)
            return Vector3.zero;

        Roader lastRoader = _storage.ActiveRoads[_storage.ActiveRoads.Count - 1];
        Vector3 position = lastRoader.End.position - newRoader.Begin.localPosition;

        return position;
    }

    public void ReturnRoad(Roader road)
    {
        ReturnObject(road);
        _storage.RemoveRoad(road);
    }
}