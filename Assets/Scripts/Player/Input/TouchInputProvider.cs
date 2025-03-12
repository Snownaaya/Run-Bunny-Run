using UnityEngine.InputSystem;

public class TouchInputProvider : IInputProvider
{
    private Character _character;

    public TouchInputProvider(Character character) =>
        _character = character;

    public InputAction MoveRight => _character.PlayerInput.Character.SwipeRight;
    public InputAction MoveLeft => _character.PlayerInput.Character.SwipeLeft;
    public InputAction MoveDown => _character.PlayerInput.Character.SwipeDown;
}
