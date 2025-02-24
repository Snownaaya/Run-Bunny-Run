using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody), typeof(PlayerInput), typeof(PlayerCollisionHandler))]
[RequireComponent(typeof(Animator), typeof(PlayerAudio))]
public class Player : MonoBehaviour, IMoveble
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _checkRaduis;

    [SerializeField] public LayerMask _groundMask;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private ParticleSystem _particle;

    private PlayerJumper _playerJumper;
    private PlayerMovement _playerMovement;
    private PlayerAudio _playerAudio;
    private PlayerCollisionHandler _playerCollision;
    private Animator _animator;
    private Transform _transform;

    private Vector2 _moveDirection;

    private bool _isJumping;

    public Transform Transform => _transform;
    public float Speed => _speed;

    public float JumpForce => _jumpForce;

    public event Action GameOver;

    private void Awake()
    {
        _transform = transform;
        _playerCollision = GetComponent<PlayerCollisionHandler>();
        _playerAudio = GetComponent<PlayerAudio>();
        _animator = GetComponent<Animator>();

        _playerMovement = new PlayerMovement(this);
        _playerJumper = new PlayerJumper(_groundMask, _targetPoint, GetComponent<Rigidbody>(),
        this, _checkRaduis, _animator, _playerAudio, _particle);
        _playerAudio.Initialize(_animator);
    }

    private void FixedUpdate()
    {
        _playerMovement.Move();
        _playerJumper.CheckLanding();
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
