using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTester : MonoBehaviour, IProjectileKillable, IForceReceivable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReceiveProjectileHit(ProjectileBase Projectile)
    {
        Debug.Log("I WAS HIT BY" + Projectile.name);
    }

    public void ReceiveForce(GameObject source, float force, float radius)
    {
    }
}
