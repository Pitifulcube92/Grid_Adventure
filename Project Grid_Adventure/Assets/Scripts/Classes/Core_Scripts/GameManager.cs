using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SoundManager audioSys;
    [SerializeField] private UIManager uiSys;
    [SerializeField] private LevelManager lvlSys;

    public static GameManager instance
    {
        get;
        private set;
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if(SetUpManagers() == false)
        {
            Debug.LogError("GameManager has hit a critical error!");
            Application.Quit();
        }
        IntializeApplication();
    }
    private void IntializeApplication()
    {
        //Run MainMenu, Play music;
        audioSys.SetBGMVolume(audioSys.GetBGMVolume());
        audioSys.SetSFXVolume(audioSys.GetSFXVolume());
        audioSys.SetLoopBGM(true);
    }
    private bool SetUpManagers()
    {
        if(!audioSys)
            audioSys = new SoundManager();
        if(!lvlSys)
            lvlSys = new LevelManager();
        if (!uiSys)
            uiSys = new UIManager();

        if (audioSys == null || lvlSys == null || uiSys == null)
        {
            return false;
        }
            
        return true;
    }

    public void ToggleFullScreen(bool tmp_)
    {
        Debug.Log(tmp_);
        if (tmp_)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    public UIManager GetUIManager() { return uiSys; }
    public SoundManager GetSoundManager() { return audioSys; }
    public LevelManager GetLevelManager() { return lvlSys; }
}
