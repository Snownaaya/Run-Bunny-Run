using System;
using UnityEngine;

[RequireComponent(typeof(PlayerCollisionHandler), typeof(CharacterController), typeof(PlayerAudio))]
[RequireComponent(typeof(ClosestRoadTracker))]
public class Character : MonoBehaviour
{
    [SerializeField] private CharacterView _view;
    [SerializeField] private CharacterConfig _characterConfig;
    [SerializeField] private GroundCheck _groundCheck;
    [SerializeField] private RoaderStorage _roaderStorage;
    [SerializeField] private HandleRoadSpeed _roadSpeedHandler;

    private IInputProvider _inputProvider;
    private Vector3 _initialPosition;

    private CharacterStateMachine _stateMachine;
    private CharacterController _characterController;
    private PlayerCollisionHandler _playerCollision;
    private PlayerAudio _playerAudio;
    private ClosestRoadTracker _roadTracker;

    private bool _isDead;

    private void Awake()
    {
        _initialPosition = transform.position;
        _view.Initialize();
        _characterController = GetComponent<CharacterController>();
        _playerAudio = GetComponent<PlayerAudio>();
        _playerCollision = GetComponent<PlayerCollisionHandler>();
        _roadTracker = GetComponent<ClosestRoadTracker>();
        _inputProvider = InputProviderFactory.GetInputProvider();
        _stateMachine = new CharacterStateMachine(this, _inputProvider);
    }

    private void Start() =>
        _roadTracker.StartTracking(transform);

    public CharacterConfig CharacterConfig => _characterConfig;
    public CharacterController CharacterController => _characterController;
    public CharacterView CharacterView => _view;
    public GroundCheck GroundCheck => _groundCheck;
    public PlayerAudio PlayerAudio => _playerAudio;

    public event Action GameOver;

    private void Update()
    {
        if (!_isDead)
        {
            _stateMachine.HandleInput();
            _stateMachine.Update();
        }
    }

    private void OnEnable() =>
        _playerCollision.CollisionDetected += ProccesColision;

    private void OnDisable() =>
        _playerCollision.CollisionDetected -= ProccesColision;

    public void Reset()
    {
        transform.position = _initialPosition;
        _isDead = false;
        _characterController.enabled = true;
        _stateMachine.ResetVelocity();
        _roadTracker.SetLastRevivePoint(null);
    }

    public void Revive()
    {
        OnReviveStart();
        _isDead = false;
        _characterController.enabled = false;

        Roader closestRoad = _roadTracker.ClosestRoad;

        if (closestRoad != null)
        {
            transform.position = closestRoad.RevivePoint.position + Vector3.up * 1f;
            _roadTracker.SetLastRevivePoint(closestRoad.RevivePoint);
        }

        _characterController.enabled = true;
        _stateMachine.ResetVelocity();
        _roadSpeedHandler.SyncSpeedAfterRevive();
    }

    private void ProccesColision(IInteractable interactable)
    {
        if (interactable is GameOverZone && !_isDead)
        {
            _isDead = true;
            _characterController.enabled = false;
            _roadTracker.StopTracking();
            GameOver?.Invoke();
        }
    }

    private void OnReviveStart()
    {
        if (_isDead)
            _roadTracker.StartTracking(transform);
    }
}