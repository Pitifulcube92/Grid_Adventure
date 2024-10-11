using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("System")]
    [SerializeField] private SoundManager audioSys;
    [SerializeField] private UIManager uiSys;
    [SerializeField] private LevelManager lvlSys;
    [SerializeField] private SaveManager SaveSys;
    [Header("Info")]
    [SerializeField] private int CurrentLevel;
    [SerializeField] private Gamemode_State currentGM;
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
    
        //Get or Initalize Save Data;
        if(PlayerPrefs.HasKey("CurrentLevel") == true && PlayerPrefs.GetInt("CurrentLevel") > 1)
        {
            CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
        }
        else
        {
            CurrentLevel = 1;
            SaveSys.SaveProgress(CurrentLevel);
        }
       
    }
    private bool SetUpManagers()
    {
        if(!audioSys)
            audioSys = new SoundManager();
        if(!lvlSys)
            lvlSys = new LevelManager();
        if (!uiSys)
            uiSys = new UIManager();
        if (!SaveSys)
            SaveSys = new SaveManager();

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
    public SaveManager GetSaveManager() { return SaveSys; }
    public int GetCurrentLevel() { return CurrentLevel; }
    public void SetCurrnetLevel(int tmp_) { CurrentLevel = tmp_; }
    public Gamemode_State GetGamemode() { return currentGM; }
    public void SetGamemode(int tmp_)
    {
        switch (tmp_) {

            case 0:
                currentGM = Gamemode_State.Story;
                break;
            case 1:
                currentGM = Gamemode_State.LevelSelect;
                break;
        }
    }
}
