using UnityEngine;

public class WalkBehindTransparent : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Color color = spriteRenderer.color;
            color.a = 0.5f; // 50% transparan
            spriteRenderer.color = color;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Color color = spriteRenderer.color;
            color.a = 1f; // Kembali normal
            spriteRenderer.color = color;
        }
    }
}
