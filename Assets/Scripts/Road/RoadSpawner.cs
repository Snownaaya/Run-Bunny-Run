using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : ObjectPool<Roader>, IResetteble
{
    [SerializeField] private Player _player;
    [SerializeField] private List<Roader> _roadersList = new List<Roader>();
    [SerializeField] private CoinSpawner _coinSpawner;

    [SerializeField] private ScoreCounter _scoreCounter;

    private float _spawnDistance = 35f;
    private int _checkSpawnCount = 3;


    private void Start() =>
        StartCoroutine(GeneratorRoad());

    private IEnumerator GeneratorRoad()
    {
        while (enabled)
        {
            yield return null;
            if (_roadersList.Count > 0)
            {
                if (_player.transform.position.z >= _roadersList[_roadersList.Count - 1].Begin.position.z - _spawnDistance)
                    Spawn(_roadersList[0]);
            }
        }
    }

    private void Spawn(Roader roader)
    {
        Roader newRoader = GetObject(roader);
        _roadersList.Add(newRoader);

        newRoader.transform.position = _roadersList[_roadersList.Count - 1].Begin.localPosition - newRoader.End.localPosition;
        newRoader.gameObject.SetActive(true);

        newRoader.Init(_scoreCounter, _coinSpawner);

        CheckAndReturnOldRoader();
    }

    private void CheckAndReturnOldRoader()
    {
        if (_roadersList.Count > _checkSpawnCount)
        {
            Roader oldRoader = _roadersList[0];
            _roadersList.RemoveAt(0);
            ReturnObject(oldRoader);
        }
    }
}
