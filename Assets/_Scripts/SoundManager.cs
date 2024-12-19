using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource soundsSource;
    public AudioSource musicSource;

    [Space]

    public AudioClip musicAmbiente;
    public AudioClip[] pops;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (SoundSettingsManager.Instance != null)
        {
            SoundSettingsManager.Instance.RegisterSoundManager(this);
        }

        MusicAmbiente();
    }

    public void MusicAmbiente()
    {
        musicSource.Play();
    }

    public void PopSound()
    {
        int rndIndex = Random.Range(0, pops.Length);

        soundsSource.PlayOneShot(pops[rndIndex]);
    }
}
