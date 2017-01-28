using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyWalls : MonoBehaviour, IProjectileKillable
{
    public void ReceiveProjectileHit(ProjectileBase Projectile)
    {
        
    }

    public bool CanBeKilled()
    {
        return false;
    }
}
