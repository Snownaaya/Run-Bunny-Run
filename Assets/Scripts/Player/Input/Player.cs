using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IResetteble
{
    [SerializeField] private float _speed = 5.0f;
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

    public event Action GameOver;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _rigidbody = GetComponent<Rigidbody>();
        _playerCollision = GetComponent<PlayerCollisionHandler>();

        _playerInput.Player.Jump.performed += OnJump;
        _playerInput.Player.Jump.canceled -= OnJump;

        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    private void Start() =>
        StartCoroutine(Movement());

    private void Update()
    {
        _moveDirection = _playerInput.Player.Move.ReadValue<Vector2>();
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
            yield return null;
        }
    }
}
