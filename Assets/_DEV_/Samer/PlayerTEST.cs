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
            if (createdBomb == null)
            {
                createdBomb = bombPlacer.CreateBomb(this.transform.position + new Vector3(15f, 0, 0));
            }
            else if (createdBomb.CanKick(transform.position))
            {
                createdBomb.StartKick(transform);
            }
        }      
    }
}

