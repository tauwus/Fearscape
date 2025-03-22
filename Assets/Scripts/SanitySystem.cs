using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class SanitySystem : MonoBehaviour
{
    public float minSanity = 100f; 
    public float maxSanity = 200f; 
    private float currentSanity;
    private float sanityIncreaseRate = 3f; 
    public TMP_Text sanityText;
    public GameObject warningPanel;
    private Rigidbody2D rb;

    void Start()
    {
        currentSanity = minSanity;
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D found on Player! Knockback will not work.");
        }

        InvokeRepeating("IncreaseSanityPerSecond", 1f, 1f);
        UpdateSanityText();

        if (warningPanel != null)
        {
            warningPanel.SetActive(false);
        }
    }

    void IncreaseSanityPerSecond()
    {
        IncreaseSanity(sanityIncreaseRate);
    }

    public void IncreaseSanity(float amount, Vector2? knockbackDirection = null, float knockbackForce = 10f)
    {
        currentSanity += amount;
        currentSanity = Mathf.Min(currentSanity, maxSanity); 
        UpdateSanityText();

        if (currentSanity >= 175) 
        {
            StartCoroutine(ShowWarningPanel());
        }

        if (currentSanity >= maxSanity)
        {
            SceneManager.LoadScene("Game Over");
        }

        if (knockbackDirection.HasValue && rb != null)
        {
            Debug.Log("Applying knockback in direction: " + knockbackDirection.Value);
            rb.linearVelocity = Vector2.zero; // Stop movement before applying force
            rb.AddForce(knockbackDirection.Value * knockbackForce, ForceMode2D.Impulse);
        }
    }

    public void DecreaseSanity(float amount)
    {
        currentSanity = Mathf.Max(currentSanity - amount, minSanity); 
        UpdateSanityText();
    }

    void UpdateSanityText()
    {
        if (sanityText != null)
        {
            sanityText.text = currentSanity + " BPM";
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
