using UnityEngine;
using System.Collections;

public class Skeleton : MonoBehaviour
{
    public int bpmIncreaseAmount = 5; // Increase BPM instead of draining it
    public GameObject warningPanel; // Assign this in the Inspector
    private SpriteRenderer spriteRenderer; // Reference to the sprite renderer

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false; // Hide the skeleton initially
        }

        if (warningPanel != null)
        {
            warningPanel.SetActive(false); // Ensure it's hidden at start
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = true; // Show the skeleton
            }

            SanitySystem sanitySystem = other.GetComponent<SanitySystem>();
            if (sanitySystem != null)
            {
                sanitySystem.IncreaseSanity(bpmIncreaseAmount); // Increase BPM
                StartCoroutine(ShowWarningPanel());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false; // Hide the skeleton when player leaves
            }
        }
    }

    private IEnumerator ShowWarningPanel()
    {
        if (warningPanel != null)
        {
            warningPanel.SetActive(true);
            yield return new WaitForSeconds(1f); // Show panel for 1 second
            warningPanel.SetActive(false);
        }
    }
}