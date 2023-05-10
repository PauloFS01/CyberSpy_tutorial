using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource backGroundMusic;

    public AudioSource[] SFXs;

    private void Awake()
    {
        instance = this;
    }

    public void StopBackgroundMusic()
    {
        backGroundMusic.Stop();
    }

    public void PlayerSFX(int sfxNumber)
    {
        SFXs[sfxNumber].Play();
    }
}
