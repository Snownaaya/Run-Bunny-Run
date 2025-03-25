using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerCollisionHandler), typeof(CharacterController), typeof(PlayerAudio))]
public class Character : MonoBehaviour
{
    [SerializeField] private CharacterView _view;
    [SerializeField] private CharacterConfig _characterConfig;
    [SerializeField] private GroundCheck _groundCheck;

    private IInputProvider _inputProvider;
    private Vector3 _initialPosition;

    private CharacterStateMachine _stateMachine;
    private CharacterController _characterController;
    private PlayerCollisionHandler _playerCollision;
    private PlayerAudio _playerAudio;

    private void Awake()
    {
        _view.Initialize();
        _characterController = GetComponent<CharacterController>();
        _playerAudio = GetComponent<PlayerAudio>();
        _playerCollision = GetComponent<PlayerCollisionHandler>();
        _inputProvider = InputProviderFactory.GetInputProvider();
        _stateMachine = new CharacterStateMachine(this, _inputProvider);
    }

    public CharacterConfig CharacterConfig => _characterConfig;
    public CharacterController CharacterController => _characterController;
    public CharacterView CharacterView => _view;
    public GroundCheck GroundCheck => _groundCheck;
    public PlayerAudio PlayerAudio => _playerAudio;
    public IInputProvider InputProvider => _inputProvider;

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

    public void Reset() =>
        transform.position = _initialPosition;

    private void ProccesColision(IInteractable interactable)
    {
        if (interactable is GameOverZone)
            GameOver?.Invoke();
    }
}