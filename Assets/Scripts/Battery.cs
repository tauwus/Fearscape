using UnityEngine;

public class Battery : MonoBehaviour
{
    private bool playerInRange = false;
    public GameObject pickupPanel; // Assign a UI Panel in Unity

    void Start()
    {
        if (pickupPanel != null)
        {
            pickupPanel.SetActive(false); // Hide panel initially
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            FindFirstObjectByType<Flashlight>().AddBattery();
            Destroy(gameObject); // Destroy the battery object
            if (pickupPanel != null)
            {
                pickupPanel.SetActive(false); // Hide UI panel
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (pickupPanel != null)
            {
                pickupPanel.SetActive(true); // Show UI panel when player enters
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (pickupPanel != null)
            {
                pickupPanel.SetActive(false); // Hide UI panel when player leaves
            }
        }
    }
}
