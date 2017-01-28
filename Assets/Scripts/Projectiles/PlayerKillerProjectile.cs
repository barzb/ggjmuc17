using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class PlayerKillerProjectile : ProjectileBase
{
    private new Renderer renderer;

    public Color NormalColor = Color.white;
    public Color DefaultDangerousColor = Color.red;
    private Color DangerousColor = Color.red;


    protected override void OnStart()
    {
        renderer = GetComponent<Renderer>();
    }

    protected override void OnUpdate()
    {
        
    }

    protected override void OnDangerousSpeedChange(bool isDangerous)
    {
        renderer.material.color = Color.Lerp(renderer.material.color, isDangerous ? DangerousColor : NormalColor, Time.deltaTime * 10f);
    }

    protected override void OnReceiveForce(GameObject source)
    {
        PlayerState playerSource = source.GetComponent<PlayerState>();
        if(playerSource != null)
        {
            DangerousColor = PlayerStatics.Get(playerSource.PlayerId).color;
        }
        else
        {
            DangerousColor = DefaultDangerousColor;
        }
    }

    protected override void OnKill(IProjectileKillable target)
    {
       
    }
}
