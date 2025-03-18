using System.Numerics;
using UnityEngine.InputSystem;

public interface IInputProvider
{
    float Move { get; }
    InputAction MoveDown { get; }
    InputAction Jump { get; }
}
