using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public int id = 0;

    private void OnDrawGizmos()
    {
        Color prevColor = Gizmos.color;
        Gizmos.color = id == 0 ? Color.red : Color.green;
        Gizmos.DrawWireCube(transform.position + Vector3.up * 10f, Vector3.one * 20f);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * 80f);
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 80f, 10f);
        Gizmos.color = prevColor;
    }
}
