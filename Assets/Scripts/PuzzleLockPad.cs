using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleLockPad : MonoBehaviour
{
    public GameObject triggerPanel; // Panel that appears when entering the area
    public GameObject puzzlePanel;  // Panel with number buttons
    public GameObject errorPanel;   // Error message panel
    public Button[] numberButtons;  // 9 number buttons
    public Button submitButton;     // Submit button
    public GameObject lockedObject; // Object to be destroyed
    private List<int> activeButtons = new List<int>(); // Store active buttons
    public string correctCode = "1234";
    private Color defaultColor;
    private Color activeColor = Color.green;

    void Start()
    {
        if (puzzlePanel) puzzlePanel.SetActive(false);
        if (triggerPanel) triggerPanel.SetActive(false);
        if (errorPanel) errorPanel.SetActive(false);

        defaultColor = numberButtons[0].GetComponent<Image>().color; // Get default button color

        foreach (Button btn in numberButtons)
        {
            btn.onClick.AddListener(() => ToggleButton(btn));
        }

        submitButton.onClick.AddListener(SubmitCode);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && triggerPanel)
        {
            triggerPanel.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && triggerPanel)
        {
            triggerPanel.SetActive(false);
            if (puzzlePanel) puzzlePanel.SetActive(false);
        }
    }

    void Update()
    {
        if (triggerPanel && triggerPanel.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            triggerPanel.SetActive(false);
            if (puzzlePanel) puzzlePanel.SetActive(true);
            Time.timeScale = 0; // Stop time when puzzle panel is active
        }
    }

    void ToggleButton(Button btn)
    {
        int buttonIndex = System.Array.IndexOf(numberButtons, btn) + 1;

        if (activeButtons.Contains(buttonIndex))
        {
            activeButtons.Remove(buttonIndex);
            btn.GetComponent<Image>().color = defaultColor; // Reset color
        }
        else
        {
            activeButtons.Add(buttonIndex);
            btn.GetComponent<Image>().color = activeColor; // Turn green
        }
    }

    void SubmitCode()
    {
        activeButtons.Sort();
        string enteredCode = string.Join("", activeButtons);

        if (enteredCode == correctCode)
        {
            Destroy(lockedObject);
            if (puzzlePanel) puzzlePanel.SetActive(false);
            if (triggerPanel) triggerPanel.SetActive(false); // Disable before destroying
            Invoke("DestroyTriggerPanel", 0.1f); // Small delay before destroying
            ClosePuzzle();
        }
        else
        {
            if (errorPanel)
            {
                errorPanel.SetActive(true);
                Invoke("HideErrorPanel", 2f);
            }
            ResetButtons();
        }
    }

    void DestroyTriggerPanel()
    {
        if (triggerPanel) Destroy(triggerPanel);
    }

    void ClosePuzzle()
    {
        if (puzzlePanel) puzzlePanel.SetActive(false);
        Time.timeScale = 1; // Resume time after solving the puzzle
    }

    void ResetButtons()
    {
        activeButtons.Clear();
        foreach (Button btn in numberButtons)
        {
            btn.GetComponent<Image>().color = defaultColor;
        }
    }

    void HideErrorPanel()
    {
        if (errorPanel) errorPanel.SetActive(false);
    }
}
