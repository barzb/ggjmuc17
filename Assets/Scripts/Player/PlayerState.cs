using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // setup delegates
    public delegate void OnPlayerDiesDelegate(PlayerState player);
    public delegate void OnPlayerSpawnDelegate(PlayerState player);

    // assign delegates
    public OnPlayerDiesDelegate OnPlayerDies;
    public OnPlayerSpawnDelegate OnPlayerSpawn;

    public int PlayerId { get; set; }

    private Vector3 playerSpawnPosition;

    public void SetupPlayer(int playerId)
    {
        PlayerId = playerId;
        Renderer r = GetComponent<Renderer>();
        r.material.color = PlayerStatics.Get(playerId).color;
    }

    public void Spawn(Vector3 spawnPosition)
    {
        playerSpawnPosition = spawnPosition;
        transform.position = playerSpawnPosition;
        OnPlayerSpawn(this);
    }

    
    public void Die()
    {
        OnPlayerDies(this);

    }
    

    public void Respawn()
    {
        OnPlayerSpawn(this);
    }

    public void OnWin()
    {

    }

    public void OnLose()
    {

    }
}
