using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPlaces : MonoBehaviour
{
    [SerializeField] private Roader[] _roads;
    [SerializeField] private Player _player;

    private List<Roader> _spawnRoad = new List<Roader>();

    private void Update()
    {
        RoadSpawner();
    }

    private void RoadSpawner()
    {
        Roader newRoad = Instantiate(_roads[Random.Range(0, _roads.Length)]);

        _spawnRoad.Add(newRoad);


    }
}
