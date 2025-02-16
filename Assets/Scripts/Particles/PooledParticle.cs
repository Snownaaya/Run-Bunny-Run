using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class PooledParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    public ParticleSystem ParticleSystem => _particleSystem;

    private void Awake() =>
        _particleSystem = GetComponent<ParticleSystem>();

    public void Play() =>
        _particleSystem.Play();

    public void Stop() =>
        _particleSystem?.Stop();
}
