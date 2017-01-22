using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectileKillable
{
    bool CanBeKilled();

    void ReceiveProjectileHit(ProjectileBase Projectile);
}
