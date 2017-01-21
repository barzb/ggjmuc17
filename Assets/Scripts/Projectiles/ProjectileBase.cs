using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class ProjectileBase : MonoBehaviour, IForceReceivable
{
    // -1 if no dangerous speed is needed
    public float dangerousSpeedMin = 10;

    private Rigidbody physicsBody;

	// Use this for initialization
	protected void Start () {
        physicsBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (dangerousSpeedMin < 0)
            return;

        float dangerousSpeedSqr = Mathf.Pow(dangerousSpeedMin * Time.deltaTime, 2);
        OnDangerousSpeedChange(physicsBody.velocity.sqrMagnitude > dangerousSpeedSqr);
	}

    public void ReceiveForce(GameObject source, float force, float radius)
    {
        physicsBody.AddExplosionForce(force, source.transform.position, radius, 0f, ForceMode.Impulse);
        OnReceiveForce(source);
    }

    // called every tick. indicates if the speed of this object is dangerous or not
    protected abstract void OnDangerousSpeedChange(bool isDangerous);

    protected abstract void OnReceiveForce(GameObject source);
}
