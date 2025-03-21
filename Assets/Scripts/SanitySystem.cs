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
    private Rigidbody2D rb;

    void Start()
    {
        currentSanity = maxSanity;
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D found on Player! Knockback will not work.");
        }

        InvokeRepeating("DecreaseSanity", 1f, 1f);
        UpdateSanityText();

        if (warningPanel != null)
        {
            warningPanel.SetActive(false);
        }
    }

    void DecreaseSanity()
    {
        DrainSanity(sanityDecreaseRate);
    }

    public void DrainSanity(float amount, Vector2? knockbackDirection = null, float knockbackForce = 10f)
    {
        currentSanity -= amount;
        currentSanity = Mathf.Max(currentSanity, 0);
        UpdateSanityText();

        if (currentSanity <= 25)
        {
            StartCoroutine(ShowWarningPanel());
        }

        if (currentSanity <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        // Apply knockback if direction is provided
        if (knockbackDirection.HasValue && rb != null)
        {
            Debug.Log("Applying knockback in direction: " + knockbackDirection.Value);
            rb.linearVelocity = Vector2.zero; // Stop any current movement before applying force
            rb.AddForce(knockbackDirection.Value * knockbackForce, ForceMode2D.Impulse);
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