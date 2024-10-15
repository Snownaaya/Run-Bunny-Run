using UnityEngine;
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
    private PlayerCollisionHandler _playerCollision;
    private PlayerMovement _playerMovement;

    private Vector2 _moveDirection;
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private bool _isJumping;

    public event Action GameOver;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerCollision = GetComponent<PlayerCollisionHandler>();
        _playerMovement = new PlayerMovement(GetComponent<Rigidbody>(), transform, _playerInput, _groundMask, _targetPoint, _speed, _jumpForce,
        _checkRaduis, _moveDirection, _isJumping);
    }

    private void Start()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        _playerMovement.Move();

        if (_isJumping && _playerMovement.IsGrounded())
        {
            _playerMovement.Jump();
            _isJumping = false;
        }
    }

    private void OnEnable()
    {
        _playerMovement.Enable();
        _playerCollision.CollisionDetected += ProccesColision;
    }

    private void OnDisable()
    {
        _playerMovement.Disable();
        _playerCollision.CollisionDetected -= ProccesColision;
    }

    public void Reset()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
    }

    private void ProccesColision(IInteractable interactable)
    {
        if (interactable is GameOverZone)
            GameOver?.Invoke();
    }
}
