using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BombBase : MonoBehaviour
{
    private const float EXPLOSION_COUNTDOWN = 5f; // in sec

    private float   explosionRadius = 50f,
                    explosionForce = 100f;


    public GameObject Radius;

    // TEST
    private Vector3 playerPosition = new Vector3(0, 2, 1);

    public void PlaceBomb(GameObject prefab, Vector3 playerPosition)
    {     
        ShowRadius(true);

        Invoke("Explode", EXPLOSION_COUNTDOWN);
    }

    public void KickBomb(Vector3 playerPosition)
    {
        ShowRadius(false);
    }

    public void DrawKickLine(Vector3 playerPosition)
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.SetPosition(0, new Vector3(playerPosition.x, playerPosition.y, playerPosition.z));
        lineRenderer.SetPosition(1, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z));
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
    }

    private void ShowRadius(bool isShown)
    {
        Radius.SetActive(isShown);
    }
}
