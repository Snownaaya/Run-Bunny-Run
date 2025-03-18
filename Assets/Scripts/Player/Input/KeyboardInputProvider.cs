using UnityEngine.InputSystem;

public class KeyboardInputProvider : IInputProvider
{
    private Character _character;

    public KeyboardInputProvider(Character character) =>
        _character = character;

    public InputAction MoveDown => _character.PlayerInput.CharacterPC.MoveDownPC;
    public InputAction Jump => _character.PlayerInput.CharacterPC.JumpPC;

    public float Move => _character.PlayerInput.CharacterPC.MovePC.ReadValue<float>();
}