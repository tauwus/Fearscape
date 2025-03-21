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
    private bool isPlayerInside = false; // Track if player is inside the trigger

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
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            if (triggerPanel) triggerPanel.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            if (triggerPanel) triggerPanel.SetActive(false);
            if (puzzlePanel) ClosePuzzle(); // Ensure puzzle closes when leaving
        }
    }

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            if (puzzlePanel.activeSelf)
            {
                ClosePuzzle(); // Close panel if already open
            }
            else
            {
                OpenPuzzle(); // Open panel if closed
            }
        }
    }

    void OpenPuzzle()
    {
        if (triggerPanel) triggerPanel.SetActive(false);
        if (puzzlePanel) puzzlePanel.SetActive(true);
        Time.timeScale = 0; // Stop time
    }

    void ClosePuzzle()
    {
        if (puzzlePanel) puzzlePanel.SetActive(false);
        Time.timeScale = 1; // Resume time
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
            if (triggerPanel) triggerPanel.SetActive(false);
            Invoke("DestroyTriggerPanel", 0.1f);
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
