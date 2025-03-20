using System.Collections;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;
    public bool isLocked = true;

    public GameObject closedDoorSprite;
    public GameObject openDoorSprite;
    public BoxCollider2D closedDoorCollider;

    private bool isPlayerNear = false;
    private bool isDoorOpen = false;

    private DoorManager doorManager;

    private float doorDelay = 1.92f; // Delay sebelum pintu berubah

    void Start()
    {
        doorManager = FindObjectOfType<DoorManager>();

        if (doorManager == null)
        {
            Debug.LogError("DoorManager tidak ditemukan di scene!");
        }

        // Set pintu dalam keadaan tertutup saat game dimulai
        closedDoorSprite.SetActive(true);
        openDoorSprite.SetActive(false);
        closedDoorCollider.enabled = true;
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(interactKey))
        {
            if (isLocked)
            {
                Debug.Log("Pintu terkunci!");
            }
            else
            {
                StartCoroutine(ToggleDoorWithDelay());
            }
        }
    }

    private IEnumerator ToggleDoorWithDelay()
    {
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Tekan 'E' untuk membuka/tutup pintu.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
