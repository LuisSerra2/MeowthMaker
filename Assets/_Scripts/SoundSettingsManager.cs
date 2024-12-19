using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSettingsManager : MonoBehaviour
{
    public static SoundSettingsManager Instance;

    [Range(0f, 1f)] public float musicVolume = 1f;
    [Range(0f, 1f)] public float soundVolume = 1f;
    public bool isMusicMuted = false;
    public bool isSoundMuted = false;

    private void Awake()
    {

        Instance = this;
        DontDestroyOnLoad(gameObject);


        LoadSettings();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SoundVolume", soundVolume);
        PlayerPrefs.SetInt("IsMusicMuted", isMusicMuted ? 1 : 0);
        PlayerPrefs.SetInt("IsSoundMuted", isSoundMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");

        if (PlayerPrefs.HasKey("SoundVolume"))
            soundVolume = PlayerPrefs.GetFloat("SoundVolume");

        if (PlayerPrefs.HasKey("IsMusicMuted"))
            isMusicMuted = PlayerPrefs.GetInt("IsMusicMuted") == 1;

        if (PlayerPrefs.HasKey("IsSoundMuted"))
            isSoundMuted = PlayerPrefs.GetInt("IsSoundMuted") == 1;
    }

    public void ApplySettings(AudioSource musicSource, AudioSource soundSource)
    {
        if (musicSource != null)
            musicSource.volume = isMusicMuted ? 0 : musicVolume;

        if (soundSource != null)
            soundSource.volume = isSoundMuted ? 0 : soundVolume;
    }

    public void RegisterSoundManager(SoundManager soundManager)
    {
        if (soundManager != null)
        {
            ApplySettings(soundManager.musicSource, soundManager.soundsSource);
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        SaveSettings();
    }

    public void SetSoundVolume(float volume)
    {
        soundVolume = volume;
        SaveSettings();
    }

    public void ToggleMusicMute(bool mute)
    {
        isMusicMuted = mute;
        SaveSettings();
    }

    public void ToggleSoundMute(bool mute)
    {
        isSoundMuted = mute;
        SaveSettings();
    }
}
