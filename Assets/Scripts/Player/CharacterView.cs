using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterView : MonoBehaviour
{
    [SerializeField] private LandingSpawner _landingSpawner;
    [SerializeField] private PooledParticle _pooledParticle;
    [SerializeField] private Character _character;

    private const string IsRunning = nameof(IsRunning);
    private const string IsJumping = nameof(IsJumping);
    private const string IsFalling = nameof(IsFalling);
    private const string IsGrounded = nameof(IsGrounded);
    private const string IsAirborne = nameof(IsAirborne);
    private const string IsMovement = nameof(IsMovement);

    private Animator _animator;

    private void Awake() =>
        _landingSpawner.Initialize(_pooledParticle, _character);

    public void Initialize() =>
        _animator = GetComponent<Animator>();

    public void StartRunning() => _animator.SetBool(IsRunning, true);
    public void StopRunning() => _animator.SetBool(IsRunning, false);
    public void StartGrounded() => _animator.SetBool(IsGrounded, true);
    public void StopGrounded() => _animator.SetBool(IsGrounded, false);
    public void StartJumping() => _animator.SetBool(IsJumping, true);
    public void StopJumping() => _animator.SetBool(IsJumping, false);
    public void StartFalling()
    {
        _animator.SetBool(IsFalling, true);
        _landingSpawner.Spawn();
    }
    public void StopFalling()
    {
        _animator.SetBool(IsFalling, false);
        _landingSpawner.ReturnLanding(_pooledParticle);
    }
    public void StartAirborne() => _animator.SetBool(IsAirborne, true);
    public void StopAirborne() => _animator.SetBool(IsAirborne, false);
}
