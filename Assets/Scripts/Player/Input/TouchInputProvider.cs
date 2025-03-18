using UnityEngine.InputSystem;

public class TouchInputProvider : IInputProvider
{
    private Character _character;

    public TouchInputProvider(Character character) =>
        _character = character;

    public InputAction MoveDown => _character.PlayerInput.CharacterTouch.DownSwipe;
    public InputAction Jump => _character.PlayerInput.CharacterTouch.JumpSwipe;

    public float Move => _character.PlayerInput.CharacterTouch.MoveSwipe.ReadValue<float>();
}