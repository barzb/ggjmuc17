using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BombBase : MonoBehaviour
{
    private const float EXPLOSION_COUNTDOWN = 50f, // in sec
                        MAX_KICK_STRENGTH = 500f;

    private float   explosionRadius = 50f,
                    explosionForce = 100f,
                    kickStrength,
                    kickStrengthPerFrame = 20f,
                    kickRadius = 40f;

    private new Rigidbody rigidbody;

    private Vector3 kickVelocity;

    private Transform playerTransform;

    public GameObject Radius;

    private bool bombChanneling = false;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (bombChanneling)
        {
            if (CanKick(playerTransform.position))
            {
                ChannelKick();
            }
            else
            {
                CancelKick();
            }
        }       
    }

    public void PlaceBomb(GameObject prefab, Vector3 playerPosition)
    {     
        ShowRadius(true);

        Invoke("Explode", EXPLOSION_COUNTDOWN);
    }

    public void DrawKickLine()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        Vector3 start = transform.position;
        Vector3 end = start+kickVelocity;
        start.y = end.y = 100;

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }

    public void StartKick(Transform player)
    {
        playerTransform = player;
        ShowRadius(true);
        bombChanneling = true;
    }

    private void ChannelKick()
    {
        Vector3 kickDirection = (transform.position - playerTransform.position);
        kickDirection.y = 0;
        kickDirection.Normalize();

        kickStrength += Time.deltaTime * kickStrengthPerFrame;
        kickStrength = Mathf.Clamp(kickStrength, 0, MAX_KICK_STRENGTH);

        kickVelocity = kickDirection * kickStrength;

        DrawKickLine();
    }

    public void EndKick()
    {
        rigidbody.AddForce(kickVelocity);
        CancelKick();
    }

    private void CancelKick()
    {
        kickStrength = 0;
        ShowRadius(false);
        bombChanneling = false;
    }

    private void Explode()
    {
        IEnumerable<Collider> colliders = Physics.OverlapSphere(this.transform.position, explosionRadius);

        foreach (Collider hit in colliders)
        {
            if (!hit) continue;

            IForceReceivable interfaceR = InterfaceUtility.GetInterface<IForceReceivable>(hit.gameObject);
            interfaceR.ReceiveForce(this.gameObject, explosionForce, explosionRadius);
        }

        CancelKick();
    }

    public bool CanKick(Vector3 playerPosition)
    {
        return Vector3.Distance(transform.position, playerPosition) < kickRadius;
    }

    private void ShowRadius(bool isShown)
    {
        Radius.SetActive(isShown);
    }
}
