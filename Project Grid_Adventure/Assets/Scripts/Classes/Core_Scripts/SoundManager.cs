using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Range(0.0f, 1f), SerializeField] private float BGMvolume;
    [Range(0.0f, 1f), SerializeField] private float SFXvolume;
    [SerializeField] private float maxVolume;
    [SerializeField] private List<AudioClip> sfxClips;
    [SerializeField] private List<AudioClip> musicClips;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource bgmSource;

    // Start is called before the first frame update
    void Start()
    {
        if (!SetUpDefaultConfig())
            Debug.LogWarning("An issues has been detected in the configurations!");
    }
    public bool SetUpDefaultConfig()
    {
        if (sfxSource == null || bgmSource == null)
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
        bgmSource.maxDistance = maxVolume;
        sfxSource.volume = SFXvolume;
        bgmSource.volume = BGMvolume;
        return true;
    }

    public void PlaySFXClip(string name_)
    {
        foreach(AudioClip x in sfxClips)
        {
            if(x.name == name_)
            {
                if (x.loadState == AudioDataLoadState.Failed)
                {
                    Debug.LogWarning("Error has occured in SFX audio clip!");
                    return;
                }
                //sfxSource.clip = x;
                sfxSource.PlayOneShot(x);
            }
        }      
    }

    public void PlayMusicClip(string name_)
    {
        foreach (AudioClip x in musicClips)
        {
            if (x.name == name_)
            {
                if (x.loadState == AudioDataLoadState.Failed)
                {
                    Debug.LogWarning("Error has occured in SFX audio clip!");
                    return;
                }
                bgmSource.clip = x;
                bgmSource.Play();
            }
        }
    }
    public AudioSource GetBGMSource()
    {
        return bgmSource;
    }
    public AudioSource GetSFXSource()
    {
        return sfxSource;
    }
    public void SetLoopBGM(bool tmp_)
    {
        if (tmp_)
        {
            bgmSource.loop = tmp_;
        }
        else
        {
            bgmSource.loop = tmp_;
        }
    }
    public void SetBGMVolume(float tmp_)
    {
        bgmSource.volume = tmp_;
    }
    public void SetSFXVolume(float tmp_)
    {
        sfxSource.volume = tmp_;
    }
    public float GetBGMVolume()
    {
        return BGMvolume;
    }
    public float GetSFXVolume()
    {
        return SFXvolume;
    }
    public float GetMaxBGMVolume()
    {
        return maxVolume;
    }
}
