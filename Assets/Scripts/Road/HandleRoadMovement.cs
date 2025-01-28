using System.Collections;
using UnityEngine;

public class HandleRoadMovement
{
    private RoaderStorage _storage;
    private readonly ICoroutineRunner _coroutineRunner;
    private float _delay = 1f;

    private float _speedIncrement = 1f;

    public HandleRoadMovement(RoaderStorage roaderStorage, ICoroutineRunner coroutineRunner)
    {
        _storage = roaderStorage;
        _coroutineRunner = coroutineRunner;
    }

    public void StartIncreaseSpeed() =>
        _coroutineRunner.StartCoroutine(IncreaseSpeedRoutine());

    public void DecreaseSpeed(float decrement)
    {
        foreach (Roader roader in _storage.ActiveRoads)
            roader.CurrentSpeed -= decrement;
    }

    public void ResetSpeed(float defaultSpeed = 100f)
    {
        foreach (Roader roader in _storage.ActiveRoads)
        {
            Debug.Log($"Resetting speed for roader: {roader.name} to {defaultSpeed}");
            roader.CurrentSpeed = defaultSpeed;
        }
    }


    private IEnumerator IncreaseSpeedRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_delay);
            IncreaseSpeedOfAllRoads(_speedIncrement);
        }
    }

    private void IncreaseSpeedOfAllRoads(float increment)
    {
        foreach (Roader roader in _storage.ActiveRoads)
        {
            roader.CurrentSpeed += increment;
        }
    }
}