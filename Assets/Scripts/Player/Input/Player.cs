using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _checkRaduis;
    [SerializeField] private float _fallTime = 5f;

    [SerializeField] public LayerMask _groundMask;
    [SerializeField] private Transform _targetPoint;

    private PlayerInput _playerInput;
    private Rigidbody _rigidbody;

    private Vector2 _moveDirection;
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private bool _isFalling = true;
    private float _currentFallTime;

    public event Action GameOver;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _rigidbody = GetComponent<Rigidbody>();

        _playerInput.Player.Move.performed += OnMove;
        _playerInput.Player.Move.canceled -= OnMove;

        _playerInput.Player.Jump.performed += OnJump;
        _playerInput.Player.Jump.canceled -= OnJump;
    }

    private void Start()
    {
        StartCoroutine(Movement());

        _startPosition = transform.position;
        _startRotation = transform.rotation;

        _currentFallTime = _fallTime;
    }

    private void FixedUpdate()
    {
        CheckFalling();
    }

    public void Reset()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        _isFalling = false;
    }

    private IEnumerator Movement()
    {
        while (enabled)
        {
            Move();
            yield return null;
        }
    }

    private void OnEnable() =>
        _playerInput.Enable();

    private void OnDisable() =>
        _playerInput.Disable();

    private bool IsGrounded() => Physics.CheckSphere(_targetPoint.position, _checkRaduis, _groundMask);

    private void OnMove(InputAction.CallbackContext context) => _moveDirection = context.action.ReadValue<Vector2>();

    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
            Jump();
    }

    private void Move()
    {
        if (_moveDirection.sqrMagnitude < 0.1f)
            return;

        float scaledMoveSpeed = _speed * Time.deltaTime;
        Vector3 offset = new Vector3(_moveDirection.x, 0f, _moveDirection.y) * scaledMoveSpeed;

        _rigidbody.MovePosition(_rigidbody.position + offset);
    }

    private void Jump() => _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

    private void CheckFalling()
    {
        if (IsGrounded() == false)
        {
            if (_isFalling == false)
            {
                _isFalling = true;
                _currentFallTime = _fallTime;
            }

            _currentFallTime -= Time.deltaTime;

            if (_currentFallTime < 0)
            {
                GameOver?.Invoke();
            }
        }
    }
}
