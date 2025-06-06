﻿
public class CoinParticle : CoinParticleSpawner
{
    protected override void PlayParticle(PooledParticle particle) =>
        particle.Play();

    protected override void StopParticle(PooledParticle particle) =>
        particle.Stop();
}