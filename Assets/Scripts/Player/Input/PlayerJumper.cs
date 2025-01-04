using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumper : IPlayerAction
{
    private PlayerInput _playerInput;
    private LayerMask _groundMask;
    private Transform _targetPoint;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Vector2 _touchpadJumpDirection;

    private bool _isJumping;
    private float _jumpForce;
    private float _checkRaduis;

    public PlayerJumper(PlayerInput playerInput, Vector2 jumpDirection, LayerMask groundMask, Transform targetPoint, Rigidbody rigidbody, bool isJumping, float jumpForce, float checkRaduis, Animator animator)
    {
        _playerInput = playerInput;
        _touchpadJumpDirection = jumpDirection;
        _groundMask = groundMask;
        _targetPoint = targetPoint;
        _rigidbody = rigidbody;
        _isJumping = isJumping;
        _jumpForce = jumpForce;
        _checkRaduis = checkRaduis;
        _animator = animator;
    }

    public void Enable()
    {
        _playerInput.Enable();
        _playerInput.Player.Jump.performed += OnJump;
        _playerInput.Player.Jump.canceled += OnJumpCanceled;
    }

    public void Disable()
    {
        _playerInput.Disable();
        _playerInput.Player.Jump.performed -= OnJump;
        _playerInput.Player.Jump.canceled -= OnJumpCanceled;
    }

    public void Jump()
    {
        if (_isJumping && IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _animator.SetTrigger(AnimatorData.Parameters.Jump);
            _isJumping = false;
        }
    }

    public bool IsGrounded() =>
        Physics.CheckSphere(_targetPoint.position, _checkRaduis, _groundMask);

    private void OnJump(InputAction.CallbackContext context)
    {
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            _touchpadJumpDirection = Touchscreen.current.position.ReadValue();
            _isJumping = true;
            Jump();
        }
        else if (Mouse.current.rightButton.isPressed)
        {
            _touchpadJumpDirection = Mouse.current.position.ReadValue();
            _isJumping = true;
            Jump();
        }
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        if (context.canceled)
            _isJumping = false;
    }
}
