using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class SanitySystem : MonoBehaviour
{
    public float maxSanity = 100f;
    private float currentSanity;
    private float sanityDecreaseRate = 3f;
    public TMP_Text sanityText; 
    public GameObject warningPanel; 

    void Start()
    {
        currentSanity = maxSanity;
        InvokeRepeating("DecreaseSanity", 1f, 1f);
        UpdateSanityText();
        if (warningPanel != null)
        {
            warningPanel.SetActive(false); // Ensure the panel is initially hidden
        }
    }

    void DecreaseSanity()
    {
        currentSanity -= sanityDecreaseRate;
        currentSanity = Mathf.Max(currentSanity, 0); // Prevent sanity from going below 0
        UpdateSanityText();

        if (currentSanity <= 25)
        {
            StartCoroutine(ShowWarningPanel());
        }

        if (currentSanity <= 0)
        {
            SceneManager.LoadScene("GameOver"); // Or trigger insanity effects
        }
    }

    public void RestoreSanity(float amount)
    {
        currentSanity = Mathf.Min(currentSanity + amount, maxSanity);
        UpdateSanityText();
    }

    void UpdateSanityText()
    {
        if (sanityText != null)
        {
            sanityText.text = currentSanity + "/100";
        }
    }

    private IEnumerator ShowWarningPanel()
    {
        if (warningPanel != null)
        {
            warningPanel.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            warningPanel.SetActive(false);
        }
    }
}