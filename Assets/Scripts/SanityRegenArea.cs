using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SanityRegenArea : MonoBehaviour
{
    public int decreaseAmount = 5; // Decrease BPM instead of increasing it
    public float decreaseInterval = 1f;
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
                InvokeRepeating("DecreaseSanity", 0f, decreaseInterval);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            CancelInvoke("DecreaseSanity");
        }
    }

    private void DecreaseSanity()
{
    if (playerInside && sanitySystem != null)
    {
        sanitySystem.DecreaseSanity(decreaseAmount); // Call the correct method
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
