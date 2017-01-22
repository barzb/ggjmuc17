using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YumYumProjectile : PlayerKillerProjectile
{
    public ParticleSystem NoodleExplosionParticles;

    protected override void OnKill(IProjectileKillable target)
    {
        base.OnKill(target);
        if (NoodleExplosionParticles != null)
        {
            Instantiate<ParticleSystem>(NoodleExplosionParticles, transform.position, Quaternion.identity);
        }
    }

    protected override void OnDangerousSpeedChange(bool isDangerous)
    {
        // spawn noodle emitter
    }
}
