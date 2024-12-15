using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : IPlayerAction
{
    private PlayerInput _playerInput;
    private Transform _transform;
    private Vector2 _moveDirection;

    private float _speed;

    public PlayerMovement(Transform transform, PlayerInput inputPlayer, float speed, Vector2 moveDirection)
    {
        _playerInput = inputPlayer;
        _speed = speed;
        _moveDirection = moveDirection;
        _transform = transform;
    }

    public void Enable()
    {
        _playerInput.Enable();
        _playerInput.Player.Move.performed += OnMove;
        _playerInput.Player.Move.canceled += OnMoveCanceled;
    }

    public void Disable()
    {
        _playerInput.Disable();
        _playerInput.Player.Move.performed -= OnMove;
        _playerInput.Player.Move.canceled -= OnMoveCanceled;
    }

    public void Move()
    {
        float scaledMoveSpeed = _speed * Time.deltaTime;
        Vector3 offset = new Vector3(_moveDirection.x, 0f, _moveDirection.y) * scaledMoveSpeed;
        _transform.Translate(offset);
    }

    public void OnMove(InputAction.CallbackContext context) =>
        _moveDirection = context.action.ReadValue<Vector2>();

    private void OnMoveCanceled(InputAction.CallbackContext context) =>
        _moveDirection = Vector2.zero; 
}