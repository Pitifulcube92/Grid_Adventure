using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Range(0.0f, 1f), SerializeField] private float volume;
    [SerializeField] private int maxVolume;
    [SerializeField] private Dictionary<string, AudioClip> sfxClips;
    [SerializeField] private Dictionary<string, AudioClip> musicClips;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        if (!SetUpDefaultConfig())
            Debug.LogWarning("An issues has been detected in the configurations!");
    }
    public bool SetUpDefaultConfig()
    {
        if (sfxSource == null || musicSource == null)
        {
            Debug.LogWarning("sfx source or music source is not assigned!");
            return false;
        }
        if(sfxClips.Count == 0 || musicClips.Count == 0)
        {
            Debug.LogWarning("sfx clips or music clips do not have any loaded audio!");
            return false;
        }

        sfxSource.maxDistance = maxVolume;
        musicSource.maxDistance = maxVolume;
        sfxSource.volume = volume;
        musicSource.volume = volume;
        return true;
    }

    public void PlaySFXClip(string name_)
    {
        if (sfxClips[name_].loadState == AudioDataLoadState.Failed)
        {
            Debug.LogWarning("Error has occured in SFX audio clip!");
            return;
        }
        sfxSource.clip = sfxClips[name_];
        sfxSource.Play();
    }

    public void PlayMusicClip(string name_)
    {
        if(musicClips[name_].loadState == AudioDataLoadState.Failed)
        {
            Debug.LogWarning("Error has occured in music audio clip!");
            return;
        }
        musicSource.clip = musicClips[name_];
        musicSource.Play();
    }
}
