using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeBase : MonoBehaviour
{
    private PlayerState[] connectedPlayers;
    private bool gameWasSetup = false;
    private int currentlyConnectedPlayers;

    private List<PlayerState> PlayersAlive;
    private List<PlayerState> PlayersDead;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public static GameModeBase Get()
    {
        return GameObject.FindObjectOfType<GameModeBase>();
    }

    public virtual void SetupGame(int numPlayers)
    {
        currentlyConnectedPlayers = 0;
        gameWasSetup = true;

        connectedPlayers = new PlayerState[numPlayers];
        PlayersAlive = new List<PlayerState>();
        PlayersDead = new List<PlayerState>();
    }
    
    public virtual void ConnectPlayer(PlayerController player, int playerId)
    {
        if (!gameWasSetup || currentlyConnectedPlayers >= connectedPlayers.Length)
        {
            Debug.LogError("ConnectPlayer error");
            return;
        }

        if(playerId == 1) {
            player.ListenToInput(PlayerController.Keyboard);
        } else if(playerId == 2) {
            player.ListenToInput(PlayerController.GamePadController1);
        } else {
            player.ListenToInput(PlayerController.GamePadController2);
        }

        PlayerState playerState = player.gameObject.AddComponent<PlayerState>();
        connectedPlayers[currentlyConnectedPlayers++] = playerState;
        playerState.SetupPlayer(playerId);

        playerState.OnPlayerSpawn += OnPlayerSpawns;
        playerState.OnPlayerDies += OnPlayerDies;
    }

    private PlayerSpawn FindPlayerSpawn(int playerId)
    {
        PlayerSpawn defaultSpawn = null;
        PlayerSpawn[] spawns = GameObject.FindObjectsOfType<PlayerSpawn>();
        foreach (PlayerSpawn spawn in spawns)
        {
            if(spawn.id == playerId) {
                return spawn;
            }
            if(spawn.id == 0) {
                defaultSpawn = spawn;
            }
        }
        return defaultSpawn;
    }

    public virtual void StartGame()
    {
        if (!gameWasSetup || currentlyConnectedPlayers > connectedPlayers.Length)
        {
            Debug.LogError("StartGame error");
            return;
        }

        for (int i = 0; i < currentlyConnectedPlayers; i++)
        {
            PlayerState player = connectedPlayers[i];
            if (player == null)
                continue;
            PlayerSpawn spawn = FindPlayerSpawn(player.PlayerId);
            if(spawn) {
                player.Spawn(spawn.transform.position);
            } else {
                player.Spawn(Vector3.zero);
            }
        }
    }

    public virtual void CheckGame()
    {
        if(PlayersAlive.Count == 1)
        {
            PlayersAlive[0].OnWin();
            foreach (PlayerState loser in PlayersDead) {
                loser.OnLose();
            }
            EndGame();
            return;
        }
    }

    public virtual void EndGame()
    {
        // SHOW DEBRIEF
    }

    public void OnPlayerDies(PlayerState player)
    {
        PlayersDead.Add(player);
        PlayersAlive.Remove(player);

        CheckGame();
    }

    public void OnPlayerSpawns(PlayerState player)
    {
        PlayersDead.Remove(player);
        PlayersAlive.Add(player);

        CheckGame();
    }

    public override string ToString()
    {
        return "Last man standing mode";
    }
}
