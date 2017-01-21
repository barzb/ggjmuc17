using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class ProjectileBase : MonoBehaviour, IForceReceivable
{
    // -1 if no dangerous speed is needed
    public float dangerousSpeedMin = 2;

    public bool IsDangerousSpeed { get { return (physicsBody.velocity.sqrMagnitude > dangerousSpeedMin * dangerousSpeedMin); } }

    private Rigidbody physicsBody;

	// Use this for initialization
	protected void Start () {
        physicsBody = GetComponent<Rigidbody>();
        OnStart();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (dangerousSpeedMin >= 0)
        {
            OnDangerousSpeedChange(IsDangerousSpeed);
        }

        OnUpdate();
	}

    public void ReceiveForce(GameObject source, float force, float radius)
    {
        physicsBody.AddExplosionForce(force, source.transform.position, radius, 1f, ForceMode.Impulse);
        OnReceiveForce(source);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!IsDangerousSpeed)
            return;

        IProjectileKillable projectileTarget = InterfaceUtility.GetInterface<IProjectileKillable>(collision.gameObject);
        if(projectileTarget != null) {
            projectileTarget.ReceiveProjectileHit(this);
        }

    }

    protected abstract void OnStart();

    protected abstract void OnUpdate();
    
    protected abstract void OnDangerousSpeedChange(bool isDangerous);

    protected abstract void OnReceiveForce(GameObject source);
}
