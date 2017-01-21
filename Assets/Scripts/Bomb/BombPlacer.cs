using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BombPlacer : MonoBehaviour
{
    public GameObject BombPrefab;

    private List<BombBase> placedBombs = new List<BombBase>();

    public BombBase CreateBomb(Vector3 playerPosition)
    {
        Vector3 spawnPosition = new Vector3(playerPosition.x, playerPosition.y + 10, playerPosition.z);

        GameObject bombObject = Instantiate(BombPrefab, spawnPosition, new Quaternion());
        BombBase bomb = bombObject.GetComponent<BombBase>();

        bomb.PlaceBomb(BombPrefab, playerPosition);

        return bomb;
    }
}
