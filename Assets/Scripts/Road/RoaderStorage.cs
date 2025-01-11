using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoaderStorage : MonoBehaviour
{
    [SerializeField] private List<Roader> _activeRoaders = new List<Roader>();

    private HandleRoadMovement _road;
    private ScoreCounter _scoreCounter;

    private float _speedIncreaseInterval = 1f;
    private float _speedIncreaseAmount = 1f;

    public IReadOnlyList<Roader> ActiveRoads => _activeRoaders;

    private void Awake()
    {
        _scoreCounter = new ScoreCounter();
        _road = new HandleRoadMovement(_scoreCounter);
    }

    public void AddRoad(Roader roader)
    {
        _activeRoaders.Add(roader);
        _road.AddRoad(roader);
    }

    public void RemoveRoad(Roader roader)
    {
        _activeRoaders.Remove(roader);
        _road.RemoveRoad(roader);
    }
}
