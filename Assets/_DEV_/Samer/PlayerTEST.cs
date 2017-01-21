using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(BombPlacer))]
public class PlayerTEST : MonoBehaviour
{
    private BombPlacer bombPlacer;

    private BombBase createdBomb;

    private void Start()
    {
        bombPlacer = GetComponent<BombPlacer>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            createdBomb = bombPlacer.CreateBomb(this.transform.position);
        }

        if (createdBomb != null)
        {
            createdBomb.DrawKickLine(this.transform.position);
        }
    }
}

