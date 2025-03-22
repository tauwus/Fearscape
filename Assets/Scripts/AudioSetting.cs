using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;

public class AudioSetting : MonoBehaviour
{
    private static AudioSetting instance;
    public AudioSource audioSource; // 🎵 Assign the AudioSource

    void Awake()
    {
        if (instance == null)
        {
            instance = this; // Set this as the instance
            DontDestroyOnLoad(gameObject); // Keep it across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.loop = true; // Enable looping
            audioSource.Play(); // Start playing
        }
    }
}