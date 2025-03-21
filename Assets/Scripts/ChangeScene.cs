using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName; // Scene name to load (set this in the Inspector)

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure the player is entering the trigger
        {
            SceneManager.LoadScene(sceneName); // Load the assigned scene
        }
    }
}