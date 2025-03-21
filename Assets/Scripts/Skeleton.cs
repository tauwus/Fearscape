using UnityEngine;
using System.Collections;

public class Skeleton : MonoBehaviour
{
    public int sanityDrainAmount = 5;
    public GameObject warningPanel; // Assign this in the Inspector

    private void Start()
    {
        if (warningPanel != null)
        {
            warningPanel.SetActive(false); // Ensure it's hidden at start
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SanitySystem sanitySystem = other.GetComponent<SanitySystem>();
            if (sanitySystem != null)
            {
                sanitySystem.DrainSanity(sanityDrainAmount);
                StartCoroutine(ShowWarningPanel());
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
