using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class PlayerKillerProjectile : ProjectileBase
{
    private new Renderer renderer;

    public Color NormalColor = Color.white;
    public Color DangerousColor = Color.red;

    // Use this for initialization
    new void Start () {
        base.Start();
        renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void OnDangerousSpeedChange(bool isDangerous)
    {
        Color.Lerp(renderer.material.color, isDangerous ? DangerousColor : NormalColor, Time.deltaTime);
    }

    protected override void OnReceiveForce(GameObject source)
    {
        
    }
}
