using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DebriefMenu : MonoBehaviour
{
    public Text resultText;
    public UnityEditor.SceneAsset mainMenuScene;

    private AsyncOperation unloadOperation;
    private AsyncOperation loadOperation;

    // -----------------

    public void MainMenu()
    {
        GameObject eventSystem = GameObject.Find("EventSystem");
        if(eventSystem != null) {
            Destroy(eventSystem);
        }
        unloadOperation = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        loadOperation = SceneManager.LoadSceneAsync(mainMenuScene.name);
        GameModeBase.Get().CloseGameSession();
        InvokeRepeating("OnLoadMainMenuComplete", 0.1f, 0.1f);
    }
    
    private void OnLoadMainMenuComplete()
    {
        if(loadOperation.isDone && unloadOperation.isDone)
        {
            CancelInvoke();
            Destroy(gameObject);
        }
    }

    // -----------------

    public void Retry()
    {
        unloadOperation = SceneManager.UnloadSceneAsync(GameModeBase.Get().LoadedLevelName);
        InvokeRepeating("ReloadCurrentLevel", 0.1f, 0.1f);
    }

    private void ReloadCurrentLevel()
    {
        if(unloadOperation.isDone)
        {
            CancelInvoke();
            loadOperation = SceneManager.LoadSceneAsync(GameModeBase.Get().LoadedLevelName, LoadSceneMode.Additive);
            InvokeRepeating("OnReloadComplete", 0.1f, 0.1f);
        }
    }

    private void OnReloadComplete()
    {
        if (loadOperation.isDone)
        {
            CancelInvoke();
            GameModeBase.Get().StartGame();
            gameObject.SetActive(false);
        }
    }

    // -----------------

    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(gameObject);
        GameModeBase.Get().debriefMenu = this;
        gameObject.SetActive(false);
    }

    public void Show(string gameResult)
    {
        resultText.text = gameResult;
        gameObject.SetActive(true);
    }
}
