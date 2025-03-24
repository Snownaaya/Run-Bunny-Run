using UnityEngine;
using UnityEngine.InputSystem;

public class TouchInputProvider : IInputProvider
{
    private Character _character;

    public TouchInputProvider(Character character) =>
        _character = character;

    public InputAction MoveDown => _character.PlayerInput.Character.MoveDown;
    public InputAction Jump => _character.PlayerInput.Character.Jump;

    public float Move => _character.PlayerInput.Character.Move.ReadValue<float>();
}