using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public const string Horizontal = nameof(Horizontal);
    public const KeyCode Space = KeyCode.Space;

    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _jumpForce = 8.0f;
    [SerializeField] private float _gravity = 9.8f;

    private CharacterController _characterController;
    private Vector3 _moveDirection = Vector3.zero;

    private float _verticalVelocity;

    private void Awake() => _characterController = GetComponent<CharacterController>();

    private void Update()
    {
        Move();

        if (Input.GetKey(Space))
            Jump();
    }

    private void FixedUpdate()
    {
        _verticalVelocity += _gravity * Time.fixedDeltaTime;
        _moveDirection.y = _verticalVelocity;
        _characterController.Move(_moveDirection * Time.fixedDeltaTime);
    }



    private void Move()
    {
        float horizontal = Input.GetAxis(Horizontal);

        _moveDirection = new Vector3(horizontal, 0f, 0f);
        _characterController.Move(_moveDirection * _speed * Time.deltaTime);
    }

    private void Jump() => _verticalVelocity = _jumpForce;
}
