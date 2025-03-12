using System;
using UnityEngine;

[Serializable]
public class AirbornStateConfig
{
    [SerializeField] private JumpingStateConfig _jumpingConfig;

    [field: SerializeField, Range(0, 20)] public float Speed { get; private set; }
    [field: SerializeField, Range(0, 30)] public float HorizontalSpeed { get; private set; }
    [field: SerializeField, Range(0, 5)] public float MoveDown { get; private set; }

    public JumpingStateConfig JumpingConfig => _jumpingConfig;

    public float BaseGravity =>
        2f * _jumpingConfig.MaxHeght / (_jumpingConfig.TimeToReachMaxHeaght * _jumpingConfig.TimeToReachMaxHeaght);
}
