using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audiomixer;
    public void SetMusicVolume(float MusicVolume)
    {
        audiomixer.SetFloat("music",MusicVolume);
    }
    public void SetSFXVolume(float SFX)
    {
        audiomixer.SetFloat("SFX", SFX);
    }
    public void Setfullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

}
