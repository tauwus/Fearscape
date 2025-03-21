using UnityEngine;
using System.Collections;

public class DoorManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip doorAudio; // Satu file suara untuk open & close

    [Header("Audio Timing")]
    public float openSoundStart = 0f;      // Awal suara buka
    public float openSoundLength = 1.92f;  // Durasi suara buka
    public float closeSoundStart = 1.92f;  // Awal suara tutup
    public float closeSoundLength = 1.92f; // Durasi suara tutup

    public void PlayOpenSound()
    {
        StartCoroutine(PlayClipPart(openSoundStart, openSoundLength));
    }

    public void PlayCloseSound()
    {
        StartCoroutine(PlayClipPart(closeSoundStart, closeSoundLength));
    }

    private IEnumerator PlayClipPart(float startTime, float length)
    {
        if (audioSource == null || doorAudio == null)
        {
            Debug.LogError("AudioSource atau doorAudio belum diatur di Inspector!");
            yield break;
        }

        audioSource.clip = doorAudio;
        audioSource.time = startTime;
        audioSource.Play();

        yield return new WaitForSeconds(length);

        audioSource.Stop(); // Hentikan setelah durasi tertentu
    }
}
