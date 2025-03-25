using System;
using UnityEngine.InputSystem;

public class KeyboardInputProvider : IInputProvider
{
    private PlayerInput _input = new PlayerInput();

    public KeyboardInputProvider()
    {
        _input.Enable();
        Jump.performed += ctx => JumpPressed?.Invoke();
        MoveDown.performed += ctx => MoveDownPressed?.Invoke();
    }

    ~KeyboardInputProvider()
    {
        _input.Disable();
    }

    public InputAction Jump => _input.Character.Jump;
    public InputAction MoveDown => _input.Character.MoveDown;

    public event Action JumpPressed;
    public event Action MoveDownPressed;

    public float Move => _input.Character.Move.ReadValue<float>();
}