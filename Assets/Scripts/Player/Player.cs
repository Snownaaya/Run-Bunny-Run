using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public const string Horizontal = nameof(Horizontal);
    public const KeyCode Space = KeyCode.Space;

    [SerializeField] private Transform _camera;

    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _jumpForce = 10.0f;

    private CharacterController _characterController;
    private Vector3 _moveDirection;
    // private PlayerCollisionHandler _playerCollision;
    private Vector3 _verticalVelocity;


    public event Action GameOver;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        // _playerCollision = GetComponent<PlayerCollisionHandler>();
    }

    private void Update()
    {
        Move();

        if (Input.GetKey(Space))
            Jump();

        ApplyGravity();
    }

    //private void OnEnable() => _playerCollision.CollisionDetected += ProcessCollision;

    //private void OnDisable() => _playerCollision.CollisionDetected -= ProcessCollision; 

    private void Move()
    {
        float horizontal = Input.GetAxis(Horizontal);

        _moveDirection = new Vector3(horizontal, 0f, 0f);
        _characterController.Move(_moveDirection * _speed * Time.deltaTime);
    }

    private void Jump()
    {
        _verticalVelocity = Vector3.up * _jumpForce;
    }

    private void ApplyGravity()
    {
        if (_characterController.isGrounded == false)
        {
            _verticalVelocity += Physics.gravity * Time.deltaTime;
        }

        _characterController.Move(_verticalVelocity * Time.deltaTime);
    }

    //private void ProcessCollision(IInteractable interactable)
    //{
    //    if (interactable is GameOverZone)
    //        GameOver?.Invoke();
    //}
}
