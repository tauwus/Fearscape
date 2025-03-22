using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using TMPro;

public class Flashlight : MonoBehaviour
{
    private Light2D flashlight;
    public Slider batterySlider;  
    public TMP_Text batteryCountText;
    public float batteryLife = 30f;
    private float currentBattery;
    private bool isFlashlightOn = false;
    public int batteryCount = 0;
    private bool canToggle = true;
    private SanitySystem sanitySystem; // Reference to SanitySystem
    private float bpmDecreaseTimer = 0f; // Timer for BPM decrease

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip turnOnSound;
    public AudioClip turnOffSound;

    [System.Obsolete]
    void Start()
    {
        flashlight = GetComponent<Light2D>();
        sanitySystem = FindObjectOfType<SanitySystem>(); // Get reference to SanitySystem

        if (flashlight == null)
        {
            Debug.LogError("No Light2D component found on Flashlight!");
        }

        currentBattery = batteryLife;
        UpdateUI();
    }

    void Update()
    {
    RotateTowardsMouse();

    if (Input.GetMouseButtonDown(0) && canToggle) 
    {
        ToggleFlashlight();
    }

    if (isFlashlightOn)
    {
        DrainBattery();

        // Reduce BPM exactly every 1 second
        bpmDecreaseTimer += Time.deltaTime;
        if (bpmDecreaseTimer >= 1f) // Check if 1 second has passed
        {
            bpmDecreaseTimer = 0f; // Reset timer
            if (sanitySystem != null)
            {
                sanitySystem.DecreaseSanity(2); // Decrease BPM by exactly 3
            }
        }
    }
}

    void RotateTowardsMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;

        Vector3 direction = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void DrainBattery()
    {
        currentBattery -= Time.deltaTime;
        if (currentBattery <= 0)
        {
            ChangeBattery();
        }
        UpdateUI();
    }

    void ToggleFlashlight()
    {
        if (!canToggle) return; 

        isFlashlightOn = !isFlashlightOn;
        flashlight.enabled = isFlashlightOn;

        // Play sound effect
        if (audioSource != null)
        {
            audioSource.clip = isFlashlightOn ? turnOnSound : turnOffSound;
            audioSource.Play();
        }

        // Prevent spamming
        canToggle = false;
        Invoke(nameof(EnableToggle), 0.5f);
    }

    void EnableToggle()
    {
        canToggle = true;
    }

    void ChangeBattery()
    {
        if (batteryCount > 0)
        {
            batteryCount--;
            currentBattery = batteryLife;
            isFlashlightOn = true;
            flashlight.enabled = true;
        }
        else
        {
            currentBattery = 0;
            isFlashlightOn = false;
            flashlight.enabled = false;
        }
        UpdateUI();
    }

    public void AddBattery()
    {
        batteryCount++;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (batterySlider != null)
        {
            batterySlider.value = currentBattery / batteryLife; 
        }
        if (batteryCountText != null)
        {
            batteryCountText.text = "Batteries: " + batteryCount;
        }
    }
}
