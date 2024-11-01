using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumper : IPlayerAction
{
    private PlayerInput _playerInput;
    private LayerMask _groundMask;
    private Transform _targetPoint;
    private Rigidbody _rigidbiody;

    private bool _isJumping;
    private float _jumpForce;
    private float _checkRaduis;

    public PlayerJumper(PlayerInput playerInput, LayerMask groundMask, Transform targetPoint, Rigidbody rigidbody, bool isJumping, float jumpForce, float checkRaduis)
    {
        _playerInput = playerInput;
        _groundMask = groundMask;
        _targetPoint = targetPoint;
        _rigidbiody = rigidbody;
        _isJumping = isJumping;
        _jumpForce = jumpForce;
        _checkRaduis = checkRaduis;
    }

    public void Enable()
    {
        _playerInput.Enable();
        _playerInput.Player.Jump.performed += OnJump;
    }

    public void Disable()
    {
        _playerInput.Disable();
        _playerInput.Player.Jump.performed -= OnJump;
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
}
