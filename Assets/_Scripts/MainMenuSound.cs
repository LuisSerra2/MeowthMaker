using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSound : MonoBehaviour
{
    public static MainMenuSound Instance;

    public AudioSource soundsSource;

    [Space]

    public AudioClip frog;
    public AudioClip cat;

    private void Awake()
    {
        Instance = this;
    }

    public void PopSound()
    {
        switch (ThemeManager.Instance.currentThemeIndex)
        {
            case 0:
                soundsSource.PlayOneShot(frog);
                break;
            case 1:
                soundsSource.PlayOneShot(cat);
                break;
        }
    }
}

