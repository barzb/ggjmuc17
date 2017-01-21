using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectileKillable
{
    void ReceiveProjectileHit(ProjectileBase Projectile);
}
