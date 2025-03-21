using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;

public class AudioSetting : MonoBehaviour
{
    public static AudioSetting Instance; // Singleton instance

    public AudioMixer masterMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    public List<AudioSource> musicSources = new List<AudioSource>(); // Dynamic list
    public List<AudioSource> sfxSources = new List<AudioSource>();   // Dynamic list

    void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep it across scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        LoadVolumeSettings();
    }

    public void LoadVolumeSettings()
    {
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        musicSlider.value = savedMusicVolume;
        sfxSlider.value = savedSFXVolume;

        SetMusicVolume(savedMusicVolume);
        SetSFXVolume(savedSFXVolume);
    }

    public void SetMusicVolume(float volume)
    {
        masterMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);

        foreach (AudioSource music in musicSources)
        {
            if (music != null) music.volume = volume;
        }
    }

    public void SetSFXVolume(float volume)
    {
        masterMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);

        foreach (AudioSource sfx in sfxSources)
        {
            if (sfx != null) sfx.volume = volume;
        }
    }

    public void RegisterAudioSource(AudioSource source, bool isMusic)
    {
        if (isMusic)
        {
            if (!musicSources.Contains(source))
                musicSources.Add(source);
        }
        else
        {
            if (!sfxSources.Contains(source))
                sfxSources.Add(source);
        }
    }

    public void UnregisterAudioSource(AudioSource source)
    {
        if (musicSources.Contains(source))
            musicSources.Remove(source);

        if (sfxSources.Contains(source))
            sfxSources.Remove(source);
    }
}
