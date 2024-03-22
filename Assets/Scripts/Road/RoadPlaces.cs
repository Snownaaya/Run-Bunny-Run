using System.Collections.Generic;
using UnityEngine;

public class RoadPlaces : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Roader[] _roaderPrefabs;
    [SerializeField] private Roader _firstRoader;

    private List<Roader> _roadersList = new List<Roader>();

    private float _spawnDistance = 13f;
    private int _checkSpawnCount = 3;

    private void Awake() => _roadersList.Add(_firstRoader);

    private void Update()
    {
        if (_player.position.z > _roadersList[_roadersList.Count - 1].End.position.z - _spawnDistance)
            Spawn();
    }

    private void Spawn()
    {
        Roader newRoader = Instantiate(_roaderPrefabs[Random.Range(0, _roaderPrefabs.Length)]);
        Vector3 newPosiotion = _roadersList[_roadersList.Count - 1].End.position - newRoader.Begin.localPosition;

        newPosiotion.y = _roadersList[_roadersList.Count - 1].Begin.position.y;
        newRoader.transform.position = newPosiotion;

        _roadersList.Add(newRoader);

        if (_roadersList.Count >= _checkSpawnCount && _roadersList.Count > 0)
        {
            Destroy(_roadersList[0].gameObject);
            _roadersList.RemoveAt(0);
        }
    }
}
