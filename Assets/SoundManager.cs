using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static float soundVol;

    public AudioMixer audioMixer;

    void Start()
    {

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 30);

        }
        Load();
    }


    public void setVolume(float vol)
    {
        soundVol = vol;
        audioMixer.SetFloat("Volume", soundVol);
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", soundVol);
    }

    private void Load()
    {
        soundVol = PlayerPrefs.GetFloat("musicVolume");
    }


}
