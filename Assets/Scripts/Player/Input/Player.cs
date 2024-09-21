using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody), typeof(PlayerInput), typeof(PlayerCollisionHandler))]
public class Player : MonoBehaviour, IResetteble
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _checkRaduis;

    [SerializeField] public LayerMask _groundMask;
    [SerializeField] private Transform _targetPoint;

    private PlayerInput _playerInput;
    private Rigidbody _rigidbody;
    private PlayerCollisionHandler _playerCollision;

    private Vector2 _moveDirection;
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private float _maxJumpTime = 0.01f;
    private float _jumpTime;
    private bool _isJumping;

    public event Action GameOver;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _rigidbody = GetComponent<Rigidbody>();
        _playerCollision = GetComponent<PlayerCollisionHandler>();

        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    private void Start() =>
        StartCoroutine(Movement());

    private void Update()
    {
        _playerInput.Player.Jump.performed += OnJump;
        _playerInput.Player.Move.performed += OnMove;
    }

    private void FixedUpdate()
    {
        if (_rigidbody.velocity.y < 0)
            _rigidbody.AddForce(Vector3.down * 5f, ForceMode.Acceleration);
    }


    private void OnEnable()
    {
        _playerInput.Enable();
        _playerCollision.CollisionDetected += ProccesColision;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerCollision.CollisionDetected -= ProccesColision;
    }

    public void Reset()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
    }

    private bool IsGrounded() => Physics.CheckSphere(_targetPoint.position, _checkRaduis, _groundMask);

    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _jumpTime = 0f;
            _isJumping = true;
            Jump();
        }
        else if (context.canceled)
            _isJumping = false;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.action.ReadValue<Vector2>();
    }

    private void Move()
    {
        if (_moveDirection.sqrMagnitude < 0.1f)
            return;

        float scaledMoveSpeed = _speed * Time.deltaTime;
        Vector3 offset = new Vector3(_moveDirection.x, 0f, _moveDirection.y) * scaledMoveSpeed;

        transform.Translate(offset);
    }

    private void Jump()
    {
        if (_isJumping && IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse); 
            _jumpTime += Time.deltaTime; 

            if (_jumpTime >= _maxJumpTime)
                _isJumping = false; 
        }
    }

    private void ProccesColision(IInteractable interactable)
    {
        if (interactable is GameOverZone)
            GameOver?.Invoke();
    }

    private IEnumerator Movement()
    {
        while (enabled)
        {
            Move();
            Jump();
            yield return null;
        }
    }
}
