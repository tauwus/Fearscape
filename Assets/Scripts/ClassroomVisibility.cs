using UnityEngine;

public class ClassroomVisibility : MonoBehaviour
{
    public GameObject classroomTilemap; // Tilemap Classroom
    public float transparentAlpha = 0.3f; // Transparansi saat tidak ada pemain
    public float normalAlpha = 1f; // Opaque saat pemain masuk
    private bool isPlayerInside = false;

    private void Start()
    {
        SetTilemapAlpha(transparentAlpha); // Awalnya classroom transparan
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            SetTilemapAlpha(normalAlpha); // Classroom jadi jelas
            Debug.Log("Player masuk ke Classroom!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            Debug.Log("Player keluar dari Classroom!");
            SetTilemapAlpha(transparentAlpha); // Classroom kembali transparan
        }
    }

    private void SetTilemapAlpha(float alpha)
    {
        SpriteRenderer[] renderers = classroomTilemap.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer renderer in renderers)
        {
            Color color = renderer.color;
            color.a = alpha;
            renderer.color = color;
        }
    }
}
