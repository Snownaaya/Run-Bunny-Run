using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public const string Horizontal = nameof(Horizontal);
    public const string Vertical = nameof(Vertical);
    private const KeyCode Space = KeyCode.Space;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _rotationSpeed;

    private CharacterController _characterController;

    private Vector3 _moveDirection = Vector3.zero;

    private void Awake() => _characterController = GetComponent<CharacterController>();

    private void Update()
    {
        if (_characterController.isGrounded)
        {
            Move();
            Jump();
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxis(Horizontal);
        float vertical = Input.GetAxis(Vertical);

        
    }

    private void Jump()
    {
        if (Input.GetKey(Space))
        {
            _moveDirection.y = _jumpForce;
        }
    }
}
