using System;
using UnityEngine;

[Serializable]
public class RunningStateConfig
{
    [field: SerializeField, Range(0, 30)] public float Speed { get; private set; }
}