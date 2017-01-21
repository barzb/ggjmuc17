using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;


public enum MenuButtonType { SelectPlayer, SelectMap }

public class MenuSystem : MonoBehaviour
{

    private Canvas startScreenCanvas;
    private Canvas levelSelectionCanvas;
    private Canvas playerSelectionCanvas;

    [Header("For Selection")]
    public Image selectedLevelMapImage;
    public Image selectedPlayerImage;
    private int selectedLevelMapIndex;
    private int selectedPlayerIndex;

    [Header("Level Maps")]
    public SceneAsset persistentScene;
    public List<SceneAsset> mapScenes;

    [Header("Player")]
    public List<GameObject> playerTemplates; // To-Do: List<Base Player Type > 

    [Header("Button")]
    public GameObject buttonTemplate;


    // Use this for initialization
    void Start()
    {
        // Set all canvas through child structure
        startScreenCanvas = transform.GetChild(1).GetChild(0).GetComponent<Canvas>();
        levelSelectionCanvas = transform.GetChild(2).GetChild(0).GetComponent<Canvas>();
        playerSelectionCanvas = transform.GetChild(3).GetChild(0).GetComponent<Canvas>();
        // Disable interaction on next buttons
        levelSelectionCanvas.transform.FindChild("NextButton").GetComponent<Button>().interactable = false;
        playerSelectionCanvas.transform.FindChild("NextButton").GetComponent<Button>().interactable = false;

        // Add menu buttons for map and player selection
        CreateMenuButtons();

    }
    // Update is called once per frame
    void Update()
    {
        // enable interaction of next button if selection is placed
        if (selectedLevelMapIndex > 0)
        {
            levelSelectionCanvas.transform.FindChild("NextButton").GetComponent<Button>().interactable = true;
        }
        if (selectedPlayerIndex > 0)
        {
            playerSelectionCanvas.transform.FindChild("NextButton").GetComponent<Button>().interactable = true;
        }


    }

    // ----------- Create Menu Items functions

    private void CreateMenuButtons()
    {
        GameObject mapPanel = levelSelectionCanvas.transform.FindChild("LevelMapPanel").gameObject;
        for (int i = 0; i < mapScenes.Count; i++)
        {
            GameObject newButton = Instantiate(buttonTemplate);
            newButton.transform.SetParent(mapPanel.transform);
            newButton.name = mapScenes[i].name;
            newButton.GetComponentInChildren<Text>().text = newButton.name;
            AddOnClickFunction(newButton.GetComponent<Button>(), MenuButtonType.SelectMap, i + 1);
        }

        GameObject playerPanel = playerSelectionCanvas.transform.FindChild("PlayerSelectionPanel").gameObject;
        for (int j = 0; j < playerTemplates.Count; j++)
        {
            GameObject newButton = Instantiate(buttonTemplate);
            newButton.transform.SetParent(playerPanel.transform);
            newButton.name = playerTemplates[j].name;
            newButton.GetComponentInChildren<Text>().text = newButton.name;
            AddOnClickFunction(newButton.GetComponent<Button>(), MenuButtonType.SelectPlayer, j + 1);
        }


    }
    private void AddOnClickFunction(Button button, MenuButtonType buttonType, int functionParameter)
    {
        switch (buttonType)
        {
            case MenuButtonType.SelectMap:
                button.onClick.AddListener(() => SelectLevelMap(functionParameter));
                break;
            case MenuButtonType.SelectPlayer:
                button.onClick.AddListener(() => SelectPlayer(functionParameter));
                break;
        }
    }

    // ----------- Button OnClick() functions
    public void StartGame()
    {
        GoNext(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SelectLevelMap(int mapIndex)
    {
        selectedLevelMapIndex = mapIndex;
        string pathName = "Menu/Maps/Map" + selectedLevelMapIndex;
        Sprite levelMapSprite = Resources.Load<Sprite>(pathName);
        selectedLevelMapImage.sprite = levelMapSprite;
    }

    public void SelectPlayer(int playerIndex)
    {
        selectedPlayerIndex = playerIndex;
        string pathName = "Menu/Players/Player" + playerIndex;
        Sprite playerSprite = Resources.Load<Sprite>(pathName);
        selectedPlayerImage.sprite = playerSprite;
    }

    public void GoBack(int toCanvasBackIndex)
    {
        switch (toCanvasBackIndex)
        {
            case 0:
                startScreenCanvas.enabled = true;
                break;
            case 1:
                levelSelectionCanvas.enabled = true;
                break;
        }
    }

    public void GoNext(int toCanvasNextIndex)
    {
        switch (toCanvasNextIndex)
        {
            case 1:
                startScreenCanvas.enabled = false; break;
            case 2:

                levelSelectionCanvas.enabled = false; break;
            case 3:
                SceneManager.LoadScene(persistentScene.name);
                SceneManager.LoadScene(mapScenes[selectedLevelMapIndex - 1].name, LoadSceneMode.Additive);
                break;
        }
    }


}
