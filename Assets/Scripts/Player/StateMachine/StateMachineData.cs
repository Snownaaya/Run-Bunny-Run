using System;
using UnityEngine;

public class StateMachineData
{
    public float YVelocity;

    private float _speed;
    private float _currentLane = 1;

    public float CurrentLane
    {
        get => _currentLane;
        private set => _currentLane = value; 
    }

    public float Speed
    {
        get => _speed;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(_speed));
            _speed = value;
        }
    }

    public void ChangeLane(float direction)
    {
        float newLane = Mathf.Clamp(_currentLane + direction, 0, 2);
        _currentLane = newLane;
    }
}