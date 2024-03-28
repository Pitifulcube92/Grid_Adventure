using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Gameplay : BaseUIScript
{
    [SerializeField] public Player_Tile target_player;
    [SerializeField] public Level_Observer lvlObs;
    [SerializeField] private GameObject pauseContext;
    [SerializeField] private GameObject settingContext;

    [Header("UI Components")]
    [SerializeField] public List<Button> gameplayButtons;
    [SerializeField] public List<Text> gameplayTexts;
    [SerializeField] public List<Slider> gameplaySliders;
    [SerializeField] public List<Toggle> gameplayToggles;
    [SerializeField] public List<Image> gameplayImages;

    //[Header("Delagetes")]
    //PauseGameplay pauseGame;
    //ResumeGameplay resumeGame;

    // Start is called before the first frame update
    void Start()
    {
        if (!lvlObs)
        {
            lvlObs = GameObject.FindGameObjectWithTag("Level").GetComponent<Level_Observer>();
        }
        else
        {
            Debug.Log("Level Observer is not found!");
        }
        if (!target_player)
        {
            target_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Tile>();
        }
        else
        {
            Debug.LogError("Player was not found: UI_Gameplay Script");
        }
        if (gameplayButtons == null) { Debug.Log("missing UI"); }
        if (gameplaySliders == null) { Debug.Log("missing UI"); }
        if (gameplayToggles == null) { Debug.Log("missing UI"); }
        if (gameplayTexts == null) { Debug.Log("missing UI"); }

        foreach (Button x in GameObject.FindObjectsOfType<Button>())
        {
            //Debug.Log(x.gameObject.name);
            gameplayButtons.Add(x);
        }
        foreach (Text x in GameObject.FindObjectsOfType<Text>())
        {
            //if (x.tag == "TargetUI")
            gameplayTexts.Add(x);

        }
        foreach (Image x in GameObject.FindObjectsOfType<Image>())
        {
            //if(x.tag == "TargetUI")
            gameplayImages.Add(x);

        }
        foreach (Slider x in GameObject.FindObjectsOfType<Slider>())
        {
            gameplaySliders.Add(x);
        }
        foreach (Toggle x in GameObject.FindObjectsOfType<Toggle>())
        {
            gameplayToggles.Add(x);
        }
        pauseContext = GameObject.FindGameObjectWithTag("PauseGroup");
        settingContext = GameObject.FindGameObjectWithTag("PauseSettingGroup");

        SetUIConfigure();

    }
    override public void SetUIConfigure()
    {
        foreach (Button x in gameplayButtons)
        {
            switch (x.gameObject.name)
            {
                case "Button Restart":
                    x.onClick.AddListener(delegate { GameManager.instance.GetLevelManager().LoadScene(lvlObs.GetLevel_Info().levelName);});
                    break;
                case "Button Pause":
                    x.onClick.AddListener(delegate { PauseGameplay(); });
                    break;
                case "Button Resume":
                    x.onClick.AddListener(delegate { ResumeGameplay(); });
                    break;
                case "Button Setting":
                    x.onClick.AddListener(delegate { OpenSettings(); });
                    break;
                case "Button Exit":
                    x.onClick.AddListener(delegate { Time.timeScale = 1; GameManager.instance.GetLevelManager().LoadScene("Main Menu"); });
                    break;
                case "Button SettingBack":
                    x.onClick.AddListener(delegate { CloseSettings(); });
                    break;
            }
        }

        foreach (Text x in gameplayTexts)
        {
            switch (x.gameObject.name)
            {
                case "Level Name Text":
                    x.text = lvlObs.GetLevel_Info().levelName;
                    break;
                case "NumberOfLives":
                    x.text = lvlObs.GetLevel_Info().playerLives.ToString() + "X";
                    break;
            }
        }

        foreach (Toggle x in gameplayToggles)
        {
            switch (x.gameObject.name)
            {
                case "Fullscreen Toggle":
                    x.onValueChanged.AddListener(delegate { GameManager.instance.ToggleFullScreen(x.isOn); });
                    break;
            }
        }

        foreach (Slider x in gameplaySliders)
        {
            switch (x.gameObject.name)
            {
                case "Volume Slider":
                    break;
            }
        }

        GameObject.FindGameObjectWithTag("PauseSettingGroup").SetActive(false);
        GameObject.FindGameObjectWithTag("PauseUI").SetActive(false);

    }
    public void UpdatePlayerLives(int tmp_)
    {
        
        gameplayTexts.Find(x => x.name == "NumberOfLives").text = tmp_.ToString()+"X";
        Debug.Log("Changed Lives Text!");
        //tmp_.ToString() + "X";
    }

    public void PauseGameplay()
    {
        //Debug.Log("Pressed!");
        Time.timeScale = 0;
        gameplayButtons.Find(x => x.gameObject.name == "Button Pause").gameObject.SetActive(false);
        gameplayButtons.Find(x => x.gameObject.name == "Button Restart").gameObject.SetActive(false);
        gameplayImages.Find(x => x.gameObject.name == "SubInfo Panel").gameObject.SetActive(false);
        gameplayImages.Find(x => x.gameObject.name == "Pause Panel").gameObject.SetActive(true);
    }
    public void ResumeGameplay()
    {
        Time.timeScale = 1;
        gameplayButtons.Find(x => x.gameObject.name == "Button Pause").gameObject.SetActive(true);
        gameplayButtons.Find(x => x.gameObject.name == "Button Restart").gameObject.SetActive(true);
        gameplayImages.Find(x => x.gameObject.name == "SubInfo Panel").gameObject.SetActive(true);
        gameplayImages.Find(x => x.gameObject.name == "Pause Panel").gameObject.SetActive(false);

    }
    public void OpenSettings()
    {
        pauseContext.SetActive(false);
        settingContext.SetActive(true);
    }

    public void CloseSettings()
    {
        pauseContext.SetActive(true);
        settingContext.SetActive(false);
    }
}
