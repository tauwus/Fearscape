using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel; // Assign the settings panel in the Inspector

    public void StartGame()
    {
        SceneManager.LoadScene("Prologue"); // Change to your scene name
    }

    public void OpenSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit"); // Only visible in the editor
    }
}
