using UnityEngine;
using System.Collections;

public class Skeleton : MonoBehaviour
{
    public int bpmIncreaseAmount = 5; 
    public GameObject warningPanel; 
    private SpriteRenderer spriteRenderer; 

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }

        if (warningPanel != null)
        {
            warningPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = true;
            }

            SanitySystem sanitySystem = other.GetComponent<SanitySystem>();
            if (sanitySystem != null)
            {
                sanitySystem.IncreaseSanity(bpmIncreaseAmount); 
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
                spriteRenderer.enabled = false; 
            }
        }
    }

    private IEnumerator ShowWarningPanel()
    {
        if (warningPanel != null)
        {
            warningPanel.SetActive(true);
            yield return new WaitForSeconds(1f);
            warningPanel.SetActive(false);
        }
    }
}