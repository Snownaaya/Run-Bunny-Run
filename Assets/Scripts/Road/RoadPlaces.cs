using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RoadPlaces : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Roader[] _roaders;
    [SerializeField] private Roader _firstRoader;

    private List<Roader> _roadersList = new List<Roader>();

    private float _spawnDistance = 15;

    private void Awake()
    {
        _roadersList.Add(_firstRoader);
    }

    private void Update()
    {
        if (_player.position.z > _roadersList[_roadersList.Count - 1].End.position.z - _spawnDistance)
            Spawn();
    }

    private void Spawn()
    {
        Roader newRoader = Instantiate(_roaders[Random.Range(0, _roaders.Length)]);
        newRoader.transform.position = _roadersList[_roadersList.Count - 1].End.position - newRoader.Begin.localPosition;
        _roadersList.Add(newRoader);
    }
}
