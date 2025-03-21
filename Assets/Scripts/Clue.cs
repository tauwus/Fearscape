using UnityEngine;

public class Clue : MonoBehaviour
{
    public GameObject panel; // Assign the panel in the Inspector
    private bool isPlayerInTrigger = false;
    private bool isPanelOpen = false;

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            TogglePanel();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            ClosePanel(); 
        }
    }

    void TogglePanel()
    {
        isPanelOpen = !isPanelOpen;
        panel.SetActive(isPanelOpen);
        Time.timeScale = isPanelOpen ? 0 : 1;
    }

    void ClosePanel()
    {
        isPanelOpen = false;
        panel.SetActive(false);
        Time.timeScale = 1;
    }
}