using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBase : MonoBehaviour
{
    private const float EXPLOSION_COUNTDOWN = 15f;

    public GameObject Radius;

    private bool isDropped = false;

    // TEST
    private Vector3 playerPosition = new Vector3(0, 2, 1);

    void Update()
    {
        if(isDropped)
        {
            DrawKickLine(playerPosition);
        }
    }

    public void PlaceBomb(GameObject prefab, Vector3 playerPosition)
    {     
        ShowRadius(true);

        Invoke("Explode", EXPLOSION_COUNTDOWN);
    }

    public void KickBomb(Vector3 playerPosition)
    {
        ShowRadius(false);
    }

    private void Explode()
    {

    }

    private void ShowRadius(bool isShown)
    {
        Radius.SetActive(isShown);
    }

    private void DrawKickLine(Vector3 playerPosition)
    {
        LineRenderer lineRenderer = new LineRenderer();

        lineRenderer.SetPosition(0, new Vector3(playerPosition.x, playerPosition.y, playerPosition.z));
        lineRenderer.SetPosition(1, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z));
    } 
}
