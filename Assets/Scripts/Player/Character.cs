using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerCollisionHandler), typeof(CharacterController), typeof(PlayerAudio))]
public class Character : MonoBehaviour
{
    [SerializeField] private CharacterView _view;
    [SerializeField] private CharacterConfig _characterConfig;
    [SerializeField] private GroundCheck _groundCheck;
    [SerializeField] private float _laneDistance = 4;

    private IInputProvider _inputProvider;
    private CharacterStateMachine _stateMachine;
    private CharacterController _characterController;
    private PlayerCollisionHandler _playerCollision;
    private PlayerInput _input;
    private PlayerAudio _playerAudio;

    private void Awake()
    {
        _view.Initialize();
        _characterController = GetComponent<CharacterController>();
        _input = new PlayerInput();
        _playerAudio = GetComponent<PlayerAudio>();
        _playerCollision = GetComponent<PlayerCollisionHandler>();
        _inputProvider = InputProviderFactory.GetInputProvider(this);
        _stateMachine = new CharacterStateMachine(this, _inputProvider);
    }

    public CharacterConfig CharacterConfig => _characterConfig;
    public CharacterController CharacterController => _characterController;
    public CharacterView CharacterView => _view;
    public GroundCheck GroundCheck => _groundCheck;
    public PlayerInput PlayerInput => _input;
    public PlayerAudio PlayerAudio => _playerAudio;
    public float LaneDistance => _laneDistance;

    public event Action GameOver;

    private void Update()
    {
        _stateMachine.HandleInput();
        _stateMachine.Update();
    }

    private void OnEnable()
    {
        _playerCollision.CollisionDetected += ProccesColision;
    }

    private void OnDisable()
    {
        _playerCollision.CollisionDetected -= ProccesColision;
    }

    private void ProccesColision(IInteractable interactable)
    {
        if (interactable is GameOverZone)
            GameOver?.Invoke();
    }
}