using UnityEngine;
using UnityEngine.InputSystem;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _jumpForce = 10.0f;

    public event Action GameOver;

    //private PlayerCollisionHandler _playerCollision;
    private PlayerInput _playerInput;
    private Rigidbody _rigidbody;

    private Vector2 _moveDirection;
    private bool _jump = false;

    private void Awake()
    {
        _playerInput = new PlayerInput(); 
        _rigidbody = GetComponent<Rigidbody>();
        //_playerCollision = GetComponent<PlayerCollisionHandler>();

        _playerInput.Player.Move.performed += OnMove;
        _playerInput.Player.Move.canceled -= OnMove;

        _playerInput.Player.Jump.performed += OnJump;
        _playerInput.Player.Jump.canceled -= OnJump;
    }

    private void Update()
    {
        Move();

        if (_jump)
        {
            Jump();
            _jump = false;
        }
    }

    private void OnEnable() {
        _playerInput.Enable();
        //_playerCollision.CollisionDetected += ProcessCollision;
    }

    private void OnDisable() {
        _playerInput.Disable();
        //_playerCollision.CollisionDetected -= ProcessCollision;
    }

    private void OnMove(InputAction.CallbackContext context) => _moveDirection = context.action.ReadValue<Vector2>();

    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            _jump = true;
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

    //private void ProcessCollision(IInteractable interactable)
    //{
    //    if (interactable is GameOverZone)
    //        GameOver?.Invoke();
    //}
}
