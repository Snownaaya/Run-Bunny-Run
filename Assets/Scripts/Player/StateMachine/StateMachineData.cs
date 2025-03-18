using System;
using UnityEngine;

public class StateMachineData
{
    public float YVelocity;
    public float XVelocity;

    private float _speed;
    private float _xInput;

    public float XInput
    {
        get => _xInput;
        set => _xInput = Mathf.Clamp(value, -1f, 1f);
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
}