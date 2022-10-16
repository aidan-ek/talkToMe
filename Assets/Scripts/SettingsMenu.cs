using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SettingsMenu : MonoBehaviour
{


    public AudioMixer audioM;

    public void setVolume(float volume)
    {

        audioM.SetFloat("Volume", volume);
        Debug.Log(volume);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
