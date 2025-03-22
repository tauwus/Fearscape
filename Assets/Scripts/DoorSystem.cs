using System.Collections;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;
    public bool isLocked = true;

    public GameObject closedDoorSprite;
    public GameObject openDoorSprite;
    public BoxCollider2D closedDoorCollider;
    public GameObject triggerPanel;
    public GameObject panelLocked;

    private bool isPlayerNear = false;
    private bool isDoorOpen = false;
    private bool isInteracting = false;

    private DoorManager doorManager;

    private float doorDelay = 1.92f; // Delay sebelum pintu berubah

    void Start()
    {
        doorManager = FindFirstObjectByType<DoorManager>();

        // Set pintu dalam keadaan tertutup saat game dimulai
        closedDoorSprite.SetActive(true);
        openDoorSprite.SetActive(false);
        closedDoorCollider.enabled = true;
        triggerPanel.SetActive(false);
        panelLocked.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(interactKey) && !isInteracting)
        {
            if (isLocked)
            {
                Debug.Log("Pintu terkunci!");
                if (triggerPanel) triggerPanel.SetActive(false); 
                if (panelLocked) panelLocked.SetActive(true);
            }
            else
            {
                StartCoroutine(ToggleDoorWithDelay());
            }
        }
    }

    private IEnumerator ToggleDoorWithDelay()
    {
        isInteracting = true;
        if (isDoorOpen)
        {
            doorManager?.PlayCloseSound();
        }
        else
        {
            doorManager?.PlayOpenSound();
        }

        yield return new WaitForSeconds(doorDelay);

        if (isDoorOpen)
        {
            // TUTUP PINTU
            closedDoorSprite.SetActive(true);
            openDoorSprite.SetActive(false);
            closedDoorCollider.enabled = true;
        }
        else
        {
            // BUKA PINTU
            closedDoorSprite.SetActive(false);
            openDoorSprite.SetActive(true);
            closedDoorCollider.enabled = false;
        }

        isDoorOpen = !isDoorOpen;
        isInteracting = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (triggerPanel) triggerPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (triggerPanel) triggerPanel.SetActive(false); 
            if (panelLocked) panelLocked.SetActive(false);
        }
    }
}
