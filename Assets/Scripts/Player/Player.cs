using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public const string Horizontal = nameof(Horizontal);
    public const KeyCode Space = KeyCode.Space;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce = 8f;
    [SerializeField] private float _groundDistane = 0.4f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _rotateSpeed = 90f;
    [SerializeField] private CharacterController _characterController;

    private Vector3 _moveDirection = Vector3.right;
    private Vector3 _velocity = Vector3.zero;
    private float _currentNumber = 2f;


    private void Awake() => _characterController = GetComponent<CharacterController>();

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis(Horizontal);

        _characterController.Move(_moveDirection * _speed * Time.deltaTime * horizontal);
    }

    private void Jump()
    {
        _velocity.y += _gravity * Time.deltaTime;

        if (_characterController.isGrounded && Input.GetKeyDown(Space))
            _velocity.y = Mathf.Sqrt(_jumpForce * _currentNumber * _gravity);


        _characterController.Move(_velocity * Time.deltaTime);
    }
}
