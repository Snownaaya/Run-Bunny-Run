using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody), typeof(PlayerInput), typeof(PlayerCollisionHandler))]
[RequireComponent(typeof(Animator), typeof(PlayerAudio))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _checkRaduis;

    [SerializeField] public LayerMask _groundMask;
    [SerializeField] private Transform _targetPoint;

    private PlayerAudio _playerAudio;
    private PlayerInput _playerInput;
    private PlayerCollisionHandler _playerCollision;
    private PlayerMovement _playerMovement;
    private PlayerJumper _playerJumper;
    private Animator _animator;

    private Vector2 _moveDirection;
    private Vector2 _jumpDirection;

    private bool _isJumping;

    public event Action GameOver;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerCollision = GetComponent<PlayerCollisionHandler>();
        _playerAudio = GetComponent<PlayerAudio>();
        _animator = GetComponent<Animator>();

        _playerMovement = new PlayerMovement(transform, _playerInput, _speed, _moveDirection);
        _playerJumper = new PlayerJumper(_playerInput, _jumpDirection, _groundMask, _targetPoint, GetComponent<Rigidbody>(), _isJumping,
        _checkRaduis, _jumpForce, _animator, _playerAudio);
        _playerAudio.Initialize(_animator);
    }

    private void FixedUpdate()
    {
        if (_isJumping && _playerJumper.IsGrounded())
        {
            _playerJumper.Jump();
            _isJumping = false;
        }

        _playerMovement.Move();
    }

    private void Update()
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, _checkRaduis);
    }
}
