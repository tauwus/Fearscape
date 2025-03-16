using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SanityRegenArea : MonoBehaviour
{
    public int restoreAmount = 5;
    public float restoreInterval = 1f;
    private bool playerInside = false;
    private SanitySystem sanitySystem;
    private Light2D light2D;
    public float flickerSpeed = 0.1f;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;

    private void Start()
    {
        light2D = GetComponent<Light2D>();
        if (light2D != null)
        {
            InvokeRepeating("FlickerLight", 0f, flickerSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sanitySystem = other.GetComponent<SanitySystem>();
            if (sanitySystem != null)
            {
                playerInside = true;
                InvokeRepeating("RestoreSanity", 0f, restoreInterval);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            CancelInvoke("RestoreSanity");
        }
    }

    private void RestoreSanity()
    {
        if (playerInside && sanitySystem != null)
        {
            sanitySystem.RestoreSanity(restoreAmount);
        }
    }

    private void FlickerLight()
    {
        if (light2D != null)
        {
            light2D.intensity = Random.Range(minIntensity, maxIntensity);
        }
    }
}
