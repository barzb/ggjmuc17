using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameModeBase : MonoBehaviour
{
    public delegate void OnGameEndDelegate();
    public OnGameEndDelegate OnGameEnd;

    private PlayerState[] connectedPlayers;
    private int currentlyConnectedPlayers;

    private bool gameWasSetup = false;
    private bool gameInProgress = false;

    public bool IsGameInProgress { get { return gameInProgress; } }

    private List<PlayerState> playersAlive;
    private List<PlayerState> playersDead;
    
    private SceneAsset loadedLevel;
    public string LoadedLevelName { get { return loadedLevel.name; } }

    public DebriefMenu debriefMenu;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public static GameModeBase Get()
    {
        return GameObject.FindObjectOfType<GameModeBase>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && playersAlive.Count > 1)
        {

            playersAlive[1].Die();
        }
    }

    public virtual void SetupGame(int numPlayers, UnityEditor.SceneAsset loadedLevel)
    {
        gameInProgress = false;
        currentlyConnectedPlayers = 0;
        gameWasSetup = true;
        this.loadedLevel = loadedLevel;

        connectedPlayers = new PlayerState[numPlayers];
        playersAlive = new List<PlayerState>();
        playersDead = new List<PlayerState>();
    }
    
    public virtual void ConnectPlayer(PlayerController player, int playerId)
    {
        if (!gameWasSetup || currentlyConnectedPlayers >= connectedPlayers.Length)
        {
            Debug.LogError("ConnectPlayer error");
            return;
        }

        player.ListenToInput(PlayerStatics.Get(playerId).controls);

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
                player.Spawn(spawn.transform.position + Vector3.up * 10f);
            } else {
                player.Spawn(Vector3.zero);
            }
        }
        gameInProgress = true;
    }

    public virtual void CheckGame()
    {
        if(playersAlive.Count == 1 && gameInProgress)
        {
            playersAlive[0].OnWin();
            foreach (PlayerState loser in playersDead) {
                loser.OnLose();
            }
            
            EndGame();
        }
    }

    public virtual void EndGame()
    {
        gameInProgress = false;
        debriefMenu.Show("Player [" + playersAlive[0].PlayerId + "] won the game");
    }

    public virtual void CloseGameSession()
    {
        OnGameEnd();
        DestroyImmediate(gameObject);
    }

    public void OnPlayerDies(PlayerState player)
    {
        playersDead.Add(player);
        playersAlive.Remove(player);

        CheckGame();
    }

    public void OnPlayerSpawns(PlayerState player)
    {
        playersDead.Remove(player);
        playersAlive.Add(player);

        CheckGame();
    }

    public override string ToString()
    {
        return "Last man standing mode";
    }
}
