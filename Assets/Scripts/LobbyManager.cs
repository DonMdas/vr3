using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LobbyManager : MonoBehaviour
{
    public GameObject panelMainMenu;   // Panel 1
    public GameObject panelLevelSelect; // Panel 2

    public Button playButton;
    public Button exitButton;
    public Button backButton; // <-- New Back Button

    public Button[] levelButtons; // Buttons for "Level 1" to "Level 5"

    void Start()
    {
        // Show main menu, hide level select
        panelMainMenu?.SetActive(true);
        panelLevelSelect?.SetActive(false);

        // Hook up main buttons
        playButton.onClick.AddListener(ShowLevelPanel);
        exitButton.onClick.AddListener(ExitGame);
        backButton.onClick.AddListener(BackToMainMenu); // <-- Hook up Back button

        // Map level buttons to load scenes
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1; // Since Level names start from "Level 1"
            string sceneName = $"Level {levelIndex}";

            levelButtons[i].onClick.AddListener(() => LoadLevel(sceneName));
        }
    }

    void ShowLevelPanel()
    {
        if (panelMainMenu != null)
            panelMainMenu.SetActive(false); // Use SetActive instead of Destroy

        panelLevelSelect?.SetActive(true);
    }

    void BackToMainMenu() // <-- New method
    {
        panelLevelSelect?.SetActive(false);
        panelMainMenu?.SetActive(true);
    }

    void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

        Debug.Log("Game exited.");
    }
}
