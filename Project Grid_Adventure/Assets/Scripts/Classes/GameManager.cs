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

    public UIManager GetUIManager() { return uiSys; }
    public SoundManager GetSoundManager() { return audioSys; }
    public LevelManager GetLevelManager() { return lvlSys; }
}
