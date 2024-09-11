using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //singleton
    public static AudioManager Instance;

    [SerializeField] private AudioSource _bgMusicAudioSource;
    [SerializeField] private AudioSource _soundsAudioSource;

    [SerializeField] private AudioClip[] _audioClips; // index 0 = button click
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        else
            Destroy(this.gameObject);
        
    }
    public void ChangeMusicSoundLevel(float musicSliderVal)
    {
        _bgMusicAudioSource.volume = musicSliderVal;
        PlayerStats.musicVolume = musicSliderVal;
        JsonSaveLoad jsonSaveLoad = new JsonSaveLoad();
        jsonSaveLoad.SaveData();
    }
    public void ChangeSoundSoundLevel(float soundSliderVal)
    {
        _soundsAudioSource.volume = soundSliderVal;
        PlayerStats.sfxVolume = soundSliderVal;
        JsonSaveLoad jsonSaveLoad = new JsonSaveLoad();
        jsonSaveLoad.SaveData();
    }
    public void MusicOnOff(bool isOn, float musicSliderVal)
    {
        if (isOn)
        {
            _bgMusicAudioSource.volume = musicSliderVal;
            PlayerStats.isMusicOn = true;
        }
        else
        {
            _bgMusicAudioSource.volume = 0;
            PlayerStats.isMusicOn = false;
        }
        JsonSaveLoad jsonSaveLoad = new JsonSaveLoad();
        jsonSaveLoad.SaveData();
    }
    public void SoundsOnOff(bool isOn, float soundSliderVal)
    {
        if (isOn)
        {
            _soundsAudioSource.volume = soundSliderVal;
            PlayerStats.isSfxOn = true;
        }
        else
        {
            _soundsAudioSource.volume = 0;
            PlayerStats.isSfxOn = false;
        }
        JsonSaveLoad jsonSaveLoad = new JsonSaveLoad();
        jsonSaveLoad.SaveData();
    }
    public void ButtonClick()
    {
        _soundsAudioSource.PlayOneShot(_audioClips[0]);
    }
}
