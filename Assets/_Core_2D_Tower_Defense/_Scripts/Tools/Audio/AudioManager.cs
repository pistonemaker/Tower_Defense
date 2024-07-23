using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSound, sfxSound;
    public AudioSource musicSource, sFXSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSound, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("Music Not Exist");
        }
        else
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }
    
    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSound, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("SFX Not Exist");
        }
        else
        {
            sFXSource.PlayOneShot(sound.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sFXSource.mute = !sFXSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sFXSource.volume = volume;
    }
}

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}