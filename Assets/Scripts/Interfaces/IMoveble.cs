
using UnityEngine;

public interface IMoveble
{
    public Transform Transform { get; }
    public float Speed { get; }
    public float JumpForce { get; }
}
