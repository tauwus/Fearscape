using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel; // Assign panel di Inspector
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0; // Pause game
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1; // Resume game
        isPaused = false;
    }

    public void QuitGame()
    {
        Time.timeScale = 1; // Reset waktu sebelum keluar
        SceneManager.LoadScene("MainMenu");
    }
}
