using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource soundsSource;
    public AudioSource musicSource;

    [Space]

    public AudioClip musicAmbiente;
    public AudioClip iNeedMoreBullets;
    public AudioClip[] pops;

    public AudioClip[] frog;
    public AudioClip[] cat;


    private float timer;
    private float timerChosen;
    private bool chooseTimer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        chooseTimer = true;
        timer = 0;

        if (SoundSettingsManager.Instance != null)
        {
            SoundSettingsManager.Instance.RegisterSoundManager(this);
        }

        MusicAmbiente();
    }

    private void Update()
    {
        AnimalAmbientSound();
    }

    public void MusicAmbiente()
    {
        if (Player.Instance.gameStates == GameStates.MiniGame)
        {
            musicSource.Stop();
            musicSource.PlayOneShot(iNeedMoreBullets);
        } else
        {
            musicSource.Stop();
            musicSource.Play();
        }
    }

    public void PopSound()
    {
        int rndIndex = Random.Range(0, pops.Length);

        soundsSource.PlayOneShot(pops[rndIndex]);
    }

    private void AnimalAmbientSound()
    {
        switch (ThemeManager.Instance.currentThemeIndex)
        {
            case 0:
                TimerAnimalAmbientSound(frog);
                break;
            case 1:
                TimerAnimalAmbientSound(cat);
                break;
        }
    }

    private void TimerAnimalAmbientSound(AudioClip[] audioClip)
    {

        if (chooseTimer)
        {
            chooseTimer = false;
            timerChosen = Random.Range(3, 8);
        }
        timer += Time.deltaTime;

        if (timer >= timerChosen)
        {
            timer = 0;
            chooseTimer = true;
            int rndSound = Random.Range(0, audioClip.Length);
            soundsSource.PlayOneShot(audioClip[rndSound]);
        }
    }
}
