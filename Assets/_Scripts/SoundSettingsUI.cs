using UnityEngine;
using UnityEngine.UI;

public class SoundSettingsUI : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundSlider;
    public Toggle musicMuteToggle;
    public Toggle soundMuteToggle;

    private void Start()
    {
        if (SoundSettingsManager.Instance != null)
        {
            musicSlider.value = SoundSettingsManager.Instance.musicVolume;
            soundSlider.value = SoundSettingsManager.Instance.soundVolume;
            musicMuteToggle.isOn = SoundSettingsManager.Instance.isMusicMuted;
            soundMuteToggle.isOn = SoundSettingsManager.Instance.isSoundMuted;
        }

        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        soundSlider.onValueChanged.AddListener(OnSoundVolumeChanged);
        musicMuteToggle.onValueChanged.AddListener(OnMusicMuteToggled);
        soundMuteToggle.onValueChanged.AddListener(OnSoundMuteToggled);
    }

    private void OnMusicVolumeChanged(float volume)
    {
        if (SoundSettingsManager.Instance != null)
            SoundSettingsManager.Instance.SetMusicVolume(volume);
    }

    private void OnSoundVolumeChanged(float volume)
    {
        if (SoundSettingsManager.Instance != null)
            SoundSettingsManager.Instance.SetSoundVolume(volume);
    }

    private void OnMusicMuteToggled(bool isMuted)
    {
        if (SoundSettingsManager.Instance != null)
            SoundSettingsManager.Instance.ToggleMusicMute(isMuted);
    }

    private void OnSoundMuteToggled(bool isMuted)
    {
        if (SoundSettingsManager.Instance != null)
            SoundSettingsManager.Instance.ToggleSoundMute(isMuted);
    }
}
