public abstract class LandingSpawner : ObjectPool<PooledParticle>
{
    private PooledParticle _pooledParticle;
    private Character _character;

    public void Initialize(PooledParticle pooledParticle, Character character)
    {
        _pooledParticle = pooledParticle;
        _character = character;
    }

    public PooledParticle Spawn()
    {
        PooledParticle landing = GetObject(_pooledParticle);
        landing.transform.position = _character.transform.position;
        PlayParticle(landing);
        return landing;
    }

    public void ReturnLanding(PooledParticle pooledParticle)
    {
        ReturnObject(pooledParticle);
        StopParticle(pooledParticle);
    }

    protected abstract void PlayParticle(PooledParticle particle);

    protected abstract void StopParticle(PooledParticle particle);
}