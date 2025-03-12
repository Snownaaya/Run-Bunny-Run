using UnityEngine.InputSystem;

public class KeyboardInputProvider : IInputProvider
{
    private Character _character;

    public KeyboardInputProvider(Character character) =>
        _character = character;

    public InputAction MoveRight => _character.PlayerInput.Character.RightMovePC;
    public InputAction MoveLeft => _character.PlayerInput.Character.LeftMovePC;
    public InputAction MoveDown => _character.PlayerInput.Character.MoveDownPC;
}