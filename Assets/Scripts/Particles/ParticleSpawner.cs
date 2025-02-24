using UnityEngine;

public abstract class ParticleSpawner : ObjectPool<PooledParticle>
{
    private PooledParticle _pooledParticle;
    private Transform _parentTransform;
    private WalletSetup _walletSetup;

    public void Initialize(PooledParticle pooledParticle, WalletSetup walletSetup)
    {
        _pooledParticle = pooledParticle;
        _walletSetup = walletSetup;
    }

    public PooledParticle Spawn()
    {
        PooledParticle spawnedObject = GetObject(_pooledParticle);
        spawnedObject.transform.position = _walletSetup.transform.position;
        PlayParticle(spawnedObject);
        return spawnedObject;
    }

    public void ReturnParticle(PooledParticle pooledParticle)
    {
        ReturnObject(pooledParticle);
        pooledParticle.Stop();
    }

    protected abstract void PlayParticle(PooledParticle particle);

    protected abstract void StopParticle(PooledParticle particle);
}
