using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : ObjectPool<Roader>
{
    [SerializeField] private Player _player;
    [SerializeField] private List<Roader> _roadersList = new List<Roader>();
    [SerializeField] private Roader _firstRoader;
    [SerializeField] private ScoreCounter _scoreCounter;

    private float _spawnDistance = 35f;
    private int _checkSpawnCount = 3;

    private void Awake() =>
        _roadersList.Add(_firstRoader);

    private void Start() => StartCoroutine(GeneratorRoad());

    private IEnumerator GeneratorRoad()
    {
        while (enabled)
        {
            if (_player.transform.position.z >= _roadersList[_roadersList.Count - 1].Begin.position.z - _spawnDistance)
            {
                foreach (Roader roader in _roadersList)
                {
                    Spawn(roader);
                    break;
                }
            }

            yield return null;
        }
    }

    private void Spawn(Roader roader)
    {
        Roader newRoader = GetObject(roader);
        _roadersList.Add(newRoader);
        newRoader.transform.position = _roadersList[_roadersList.Count - 1].Begin.localPosition - newRoader.End.localPosition;
        newRoader.gameObject.SetActive(true);

        newRoader.Init(_scoreCounter);

        if (_roadersList.Count < _checkSpawnCount)
        {
            Roader oldRoader = _roadersList[3];
            _roadersList.RemoveAt(0);
            ReturnObject(oldRoader);
        }
    }
}
