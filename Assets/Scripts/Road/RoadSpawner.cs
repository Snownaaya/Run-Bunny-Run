using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RoadSpawner : ObjectPool<Roader>
{
    [SerializeField] private Roader _roader;
    [SerializeField] private Player _player;

    private RoaderStorage _storage;

    private float _spawnInterval = 15f;
    private int _checkSpawnCount = 3;
    private float _checkInterval = 2f;

    private void Awake() =>
        _storage = GetComponent<RoaderStorage>();

    private void Start()
    {
        _storage.AddRoad(_roader);

        StartCoroutine(GeneratorRoad());

        StartCoroutine(RemoveRoads());
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

    private IEnumerator RemoveRoads()
    {
        var waitForSecond = new WaitForSeconds(_checkInterval);
        while (enabled)
        {
            ReturnOldRoad();
            yield return waitForSecond;
        }
    }

    private void Spawn()
    {
        Roader randomRoad = GetRandomRoad();
        Roader newRoader = GetObject(randomRoad);

        newRoader.transform.position = CalculateRoadPosition();
        _storage.AddRoad(newRoader);
    }

    private Roader GetRandomRoad()
    {
        int randomIndex = Random.Range(0, _storage.ActiveRoads.Count);
        return _storage.ActiveRoads[randomIndex];
    }

    private Vector3 CalculateRoadPosition()
    {
        Roader lastRoader = _storage.ActiveRoads[_storage.ActiveRoads.Count - 1];
        Vector3 direction = (lastRoader.End.position - lastRoader.Begin.localPosition);
        return direction;
    }

    private void ReturnOldRoad()
    {
        for (int i = 0; i < _storage.ActiveRoads.Count; i++)
        {
            Roader oldRoader = _storage.ActiveRoads[0];

            if (_player.transform.position.z <= oldRoader.End.position.z)
            {
                ReturnObject(oldRoader);
                _storage.RemoveRoad(oldRoader);
                break;
            }
        }
    }
}