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
    [SerializeField] private AudioSource sfxSource1;
    [SerializeField] private AudioSource sfxSource2;
    [SerializeField] private AudioSource bgmSource;

    // Start is called before the first frame update
    void Start()
    {
        if (!SetUpDefaultConfig())
            Debug.LogWarning("An issues has been detected in the configurations!");
    }
    public bool SetUpDefaultConfig()
    {
        if (sfxSource1 == null || bgmSource == null || sfxSource2 == null)
        {
            Debug.LogWarning("sfx source or music source is not assigned!");
            return false;
        }
        if(sfxClips.Count == 0 || musicClips.Count == 0)
        {
            Debug.LogWarning("sfx clips or music clips do not have any loaded audio!");
            return false;
        }

        sfxSource1.maxDistance = maxVolume;
        sfxSource2.maxDistance = maxVolume;
        bgmSource.maxDistance = maxVolume;
        sfxSource2.volume = SFXvolume;
        bgmSource.volume = BGMvolume;
        return true;
    }

    public void PlaySFXClip(string name_, bool isPitched_, AudioSource audioSource_)
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
                if(isPitched_ == true)
                {
                    audioSource_.pitch = UnityEngine.Random.Range(1f, 1.25f);
                }
                else
                {
                    audioSource_.pitch = 1f;
                }
                audioSource_.PlayOneShot(x);
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
    public AudioSource GetSFXSource(int audioSource_)
    {
        if(audioSource_ == 1)
        {
            return sfxSource1;
        } else if (audioSource_ == 2)
        {
            return sfxSource2;
        }
        else
        {
            Debug.LogError("Could not find audioSource");
        }
        return null;
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
        sfxSource1.volume = tmp_;
        sfxSource2.volume = tmp_;
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
