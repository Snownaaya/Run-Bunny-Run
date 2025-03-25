using System;

public interface IInputProvider
{
    float Move { get; }
    event Action JumpPressed;
    event Action MoveDownPressed;
}
