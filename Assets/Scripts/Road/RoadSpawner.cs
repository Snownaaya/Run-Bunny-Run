using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : ObjectPool<Roader>
{
    [SerializeField] private Transform _player;
    [SerializeField] private List<Roader> _roadersList;
    [SerializeField] private Roader _firstRoader;
    [SerializeField] private ScoreCounter _scoreCounter;

    private float _spawnDistance = 13f;
    private int _checkSpawnCount = 3;

    private void Awake()
    {
        _roadersList = new List<Roader>();
        _roadersList.Add(_firstRoader);
    }

    private void Start() => StartCoroutine(GeneratorRoad());

    private IEnumerator GeneratorRoad()
    {
        while (enabled)
        {
            if (_player.position.z > _roadersList[_roadersList.Count - 1].End.position.z - _spawnDistance)
                Spawn();
            yield return null;
        }
    }

    private void Spawn()
    {
        Roader newRoader = GetObject();
        newRoader.gameObject.SetActive(true);
        newRoader.Init(_scoreCounter);

        SpawnPosition(newRoader);
    }

    private void SpawnPosition(Roader roader)
    {
        Vector3 newPosiotion = _roadersList[_roadersList.Count - 1].End.position - roader.Begin.localPosition;
        newPosiotion.y = _roadersList[_roadersList.Count - 1].Begin.position.y;

        roader.transform.position = newPosiotion;

        _roadersList.Add(roader);

        if (_roadersList.Count >= _checkSpawnCount)
        {
            ReturnObject(_roadersList[0]);
            _roadersList.RemoveAt(0);
        }
    }
}
