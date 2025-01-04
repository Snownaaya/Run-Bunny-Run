using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody), typeof(PlayerInput), typeof(PlayerCollisionHandler))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _checkRaduis;

    [SerializeField] public LayerMask _groundMask;
    [SerializeField] private Transform _targetPoint;

    private PlayerInput _playerInput;
    private PlayerCollisionHandler _playerCollision;
    private PlayerMovement _playerMovement;
    private PlayerJumper _playerJumper;

    private Vector2 _moveDirection;
    private Vector2 _jumpDirection;
    private Vector3 _startPosition;

    private bool _isJumping;

    public event Action GameOver;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerCollision = GetComponent<PlayerCollisionHandler>();
        _playerMovement = new PlayerMovement(transform, _playerInput, _speed, _moveDirection);
        _playerJumper = new PlayerJumper(_playerInput, _jumpDirection, _groundMask, _targetPoint, GetComponent<Rigidbody>(), _isJumping,
        _checkRaduis, _jumpForce, GetComponent<Animator>());
    }

    private void FixedUpdate()
    {
        _playerMovement.Move();

        if (_isJumping && _playerJumper.IsGrounded())
        {
            _playerJumper.Jump();
            _isJumping = false;
        }
    }

    private void OnEnable()
    {
        _playerMovement.Enable();
        _playerJumper.Enable();
        _playerCollision.CollisionDetected += ProccesColision;
    }

    private void OnDisable()
    {
        _playerMovement.Disable();
        _playerJumper.Disable();
        _playerCollision.CollisionDetected -= ProccesColision;
    }

    private void ProccesColision(IInteractable interactable)
    {
        if (interactable is GameOverZone)
            GameOver?.Invoke();
    }
}
