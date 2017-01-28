using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YumYumProjectile : PlayerKillerProjectile
{
    public ParticleSystem NoodleExplosionParticles;
    public ParticleSystem NoodleTrailParticles;

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
        if(NoodleTrailParticles == null ) {
            return;
        }
        if(isDangerous && !NoodleTrailParticles.isPlaying)
        {
            NoodleTrailParticles.Play();
            NoodleTrailParticles.gameObject.SetActive(true);
            return;
        }

        if(!isDangerous && NoodleTrailParticles.isPlaying)
        {
            NoodleTrailParticles.Stop();
            NoodleTrailParticles.gameObject.SetActive(false);
            return;
        }
    }
}
