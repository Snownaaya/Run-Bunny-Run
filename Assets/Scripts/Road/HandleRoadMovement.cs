using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleRoadMovement
{
    private List<Roader> _roaders = new List<Roader>();
    private ScoreCounter _scoreCounter;

    private float _delay = 5f;

    //private void Start()
    //{
    //    StartCoroutine(IncreasSpeed());
    //}

    public HandleRoadMovement(ScoreCounter scoreCounter) =>
        _scoreCounter = scoreCounter;

    public void Init()
    {
        
    }

    public void AddRoad(Roader roader)
    {
        _roaders.Add(roader);
        roader.RoadMoved += OnRoadMoved;
    }

    public void RemoveRoad(Roader roader)
    {
        _roaders.Remove(roader);
        roader.RoadMoved -= OnRoadMoved;
    }

    private void OnRoadMoved() =>
        _scoreCounter.IncrementScore();

    private IEnumerator IncreasSpeed()
    {
        yield return new WaitForSeconds(_delay);
        IncreaseSpeedOfAllRoads();
    }

    public void IncreaseSpeedOfAllRoads()
    {
        foreach (var roader in _roaders)
        {
            roader.IncreaseSpeed(roader.Speed);
        }
    }
}
