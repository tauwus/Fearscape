using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using TMPro;

public class Flashlight : MonoBehaviour
{
    private Light2D flashlight;
    public Slider batterySlider;  // UI Slider to show battery level
    public TMP_Text batteryCountText; // UI text to show spare batteries
    public float batteryLife = 10f;
    private float currentBattery;
    private bool isFlashlightOn = false;
    public int batteryCount = 0; // Spare batteries
    private bool canToggle = true; // Prevent spamming

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip turnOnSound;
    public AudioClip turnOffSound;

    void Start()
    {
        flashlight = GetComponent<Light2D>();
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

        if (Input.GetMouseButtonDown(0) && canToggle) // Left mouse click to toggle flashlight
        {
            ToggleFlashlight();
        }

        if (isFlashlightOn)
        {
            DrainBattery();
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
        if (!canToggle) return; // Prevent spam clicking

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
