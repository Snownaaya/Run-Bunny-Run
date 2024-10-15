using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : ObjectPool<Roader>, IResetteble
{
    [SerializeField] private List<Roader> _roadersPrefabs;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private CoinSpawner _coinSpawner;
    [SerializeField] private Roader _roader;

    private List<Roader> _roadersList = new List<Roader>();

    private float _spawnInterval = 25f;
    private int _checkSpawnCount = 3;

    private void Start()
    {
        _roadersList.Add(_roader);

        StartCoroutine(GeneratorRoad());
    }

    private IEnumerator GeneratorRoad()
    {
        var waitForSecond = new WaitForSeconds(_spawnInterval);

        while (enabled)
        {
            if (_roadersList.Count > 0)
                Spawn();

            CheckAndReturnOldRoader();
            yield return waitForSecond;
        }
    }

    private void Spawn()
    {
        Roader randomRoad = GetRandomRoad();
        Roader newRoader = GetObject(randomRoad);

        newRoader.transform.position = CalculateRoadPosition();
        newRoader.gameObject.SetActive(true);

        newRoader.Init(_scoreCounter, _coinSpawner);
        _roadersList.Add(newRoader);
    }

    private Roader GetRandomRoad()
    {
        int randomIndex = Random.Range(0, _roadersPrefabs.Count);
        return _roadersPrefabs[randomIndex];
    }

    private Vector3 CalculateRoadPosition()
    {
        Roader lastRoader = _roadersList[_roadersList.Count - 1];

        Vector3 lastEndPosition = lastRoader.End.position;

        Vector3 direction = (lastRoader.End.position - lastRoader.Begin.position).normalized;

        float roadLength = 10f;

        Vector3 newPosition = lastEndPosition + direction * roadLength;

        return newPosition;
    }

    private void CheckAndReturnOldRoader()
    {
        if (_roadersList.Count > _checkSpawnCount)
        {
            Roader oldRoader = _roadersList[0];
            ReturnObject(oldRoader);
            _roadersList.RemoveAt(0);
        }
    }
}