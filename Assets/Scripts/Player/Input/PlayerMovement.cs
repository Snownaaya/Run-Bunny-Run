using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement
{
    private Rigidbody _rigidbiody;
    private PlayerInput _playerInput;
    private LayerMask _groundMask;
    private Transform _targetPoint;
    private Transform _transform;

    private float _speed;
    private float _jumpForce;
    private float _checkRaduis;

    private Vector2 _moveDirection;
    private bool _isJumping;

    public PlayerMovement(Rigidbody rigidbody, Transform transform, PlayerInput inputPlayer, LayerMask groundMask, Transform targetPoint, float speed,
    float jumpForce, float checkRadius, Vector2 moveDirection, bool isJumping)
    {
        _rigidbiody = rigidbody;
        _playerInput = inputPlayer;
        _groundMask = groundMask;
        _targetPoint = targetPoint;
        _speed = speed;
        _jumpForce = jumpForce;
        _checkRaduis = checkRadius;
        _moveDirection = moveDirection;
        _isJumping = isJumping;
        _transform = transform;
    }

    public void Enable()
    {
        _playerInput.Enable();
        _playerInput.Player.Move.performed += OnMove;
        _playerInput.Player.Jump.performed += OnJump;
    }

    public void Disable()
    {
        _playerInput.Disable();
        _playerInput.Player.Move.performed -= OnMove;
        _playerInput.Player.Jump.performed -= OnJump;
    }

    public void Move()
    {
        float scaledMoveSpeed = _speed * Time.deltaTime;
        Vector3 offset = new Vector3(_moveDirection.x, 0f, _moveDirection.y) * scaledMoveSpeed;

        _transform.Translate(offset);
    }

    public void Jump()
    {
        if (_isJumping && IsGrounded())
        {
            _isJumping = true;
            _rigidbiody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    public bool IsGrounded() => Physics.CheckSphere(_targetPoint.position, _checkRaduis, _groundMask);

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isJumping = true;
            Jump();
        }
        else if (context.canceled)
            _isJumping = false;
    }

    public void OnMove(InputAction.CallbackContext context) =>
        _moveDirection = context.action.ReadValue<Vector2>();
}