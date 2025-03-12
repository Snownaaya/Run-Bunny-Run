using UnityEngine.InputSystem;

public interface IInputProvider
{
    InputAction MoveRight { get; }
    InputAction MoveLeft { get; }
    InputAction MoveDown { get; }
}
