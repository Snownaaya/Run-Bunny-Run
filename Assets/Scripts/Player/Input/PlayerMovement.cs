using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerMovement : IPlayerAction
{
    private PlayerInput _playerInput;
    private Transform _transform;
    private Vector2 _touchpadMoveDirection;

    private float _speed;

    public PlayerMovement(Transform transform, PlayerInput inputPlayer, float speed, Vector2 moveDirection)
    {
        _playerInput = inputPlayer;
        _speed = speed;
        _touchpadMoveDirection = moveDirection;
        _transform = transform;
    }

    public void Enable()
    {
        _playerInput.Enable();
        _playerInput.Player.Move.performed += OnMoveTouchpad;
        _playerInput.Player.Move.canceled += OnMoveCanceled;
    }

    public void Disable()
    {
        _playerInput.Disable();
        _playerInput.Player.Move.performed -= OnMoveTouchpad;
        _playerInput.Player.Move.canceled -= OnMoveCanceled;
    }

    public void Move()
    {
        float scaledMoveSpeed = _speed * Time.deltaTime;
        Vector3 offset = new Vector3(_touchpadMoveDirection.x, 0f, 0f) * scaledMoveSpeed;
        _transform.Translate(offset);
    }

    public void OnMoveTouchpad(InputAction.CallbackContext context)
    {
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.IsPressed())
        {
            _touchpadMoveDirection = Touchscreen.current.primaryTouch.delta.ReadValue();
        }
        else if (Mouse.current != null && Mouse.current.leftButton.IsPressed())
        {
            _touchpadMoveDirection = Mouse.current.delta.ReadValue();
        }
        else
        {
            _touchpadMoveDirection = Vector2.zero;
        }
    }

    private void OnMoveCanceled(InputAction.CallbackContext context) =>
        _touchpadMoveDirection = Vector2.zero; 
}