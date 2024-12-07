using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Gameplay : BaseUIScript
{
    [SerializeField] public Player_Tile target_player;
    [SerializeField] public Level_Observer lvlObs;
    [SerializeField] private GameObject pauseContext;
    [SerializeField] private GameObject settingContext;
    [SerializeField] private Camera_Follow_Component cameraRef;

    [Header("UI Components")]
    [SerializeField] public List<Button> gameplayButtons;
    [SerializeField] public List<Text> gameplayTexts;
    [SerializeField] public List<Slider> gameplaySliders;
    [SerializeField] public List<Toggle> gameplayToggles;
    [SerializeField] public List<Image> gameplayImages;
    [SerializeField] public List<Sprite> playerHealthIcons;
    [SerializeField] public GameObject DialogeUI;
    [SerializeField] public GameObject keyFragUI;
    [SerializeField] public List<GameObject> BossUIComponents;

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
        if (pauseContext == null)
            Debug.LogWarning("PauseContext not initalized");
        if (settingContext == null)
            Debug.LogWarning("SettingContext not initalized");

        gameObject.GetComponent<Canvas>().worldCamera = GameObject.FindObjectOfType<Camera>();
        gameObject.GetComponent<Canvas>().sortingLayerName = "UI";
        cameraRef = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_Follow_Component>();

       SetUIConfigure();

    }
    override public void SetUIConfigure()
    {
     
        foreach (Button x in gameplayButtons)
        {
            switch (x.gameObject.name)
            {
               case "Button Restart":             
                   x.onClick.AddListener(delegate { GameManager.instance.GetLevelManager().LoadSceneByName(SceneManager.GetActiveScene().name);});
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
                    x.onClick.AddListener(delegate { Time.timeScale = 1; GameManager.instance.GetLevelManager().LoadSceneByName("Main Menu"); });
                    break;
                case "Button SettingBack":
                    x.onClick.AddListener(delegate { CloseSettings(); });
                    break;
                case "Button Recenter":
                    x.onClick.AddListener(delegate { cameraRef.RecenterCamera(); });
                    break;
            }
            x.onClick.AddListener(delegate { GameManager.instance.GetSoundManager().PlaySFXClip("Retro_Blop_18", false, GameManager.instance.GetSoundManager().GetSFXSource(1)); });
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
                    x.isOn = Screen.fullScreen;
                    x.onValueChanged.AddListener(delegate { GameManager.instance.ToggleFullScreen(x.isOn); });
                    break;
            }
        }

        foreach (Slider x in gameplaySliders)
        {
            switch (x.gameObject.name)
            {
                case "bgm Volume Slider":
                    x.value = GameManager.instance.GetSoundManager().GetBGMVolume();
                    x.onValueChanged.AddListener(delegate { GameManager.instance.GetSoundManager().SetBGMVolume(x.value); });
                    break;
                case "sfx Volume Slider":
                    x.value = GameManager.instance.GetSoundManager().GetSFXVolume();
                    x.onValueChanged.AddListener(delegate { GameManager.instance.GetSoundManager().SetSFXVolume(x.value); });
                    break;
            }
        }
        
        foreach(GameObject x in GameObject.FindGameObjectsWithTag("BossUI"))
        {
            BossUIComponents.Add(x);
        }
        GameObject.FindGameObjectWithTag("PauseSettingGroup").SetActive(false);
        GameObject.FindGameObjectWithTag("PauseUI").SetActive(false);
        DialogeUI.SetActive(false);
        keyFragUI.SetActive(false);
        GameObject.Find("Boss_Info_3").SetActive(false);
        GameObject.Find("Boss_Info_1").SetActive(false);
        //GameObject.Find("key_Fragment_Panel").SetActive(false);

        //Check it boss lvl component is in lvl
        foreach (Base_Level_Component x in GameObject.FindObjectOfType<Level_Observer>().GetLevel_Components())
        {
            if (x.GetComponent<Boss_1_Level_Component>() == true)
            {
                Debug.Log("Boss Componenet Found!");
                BossUIComponents.Find(x => x.name == "Boss_Info_1").SetActive(true);
            }
            if (x.GetComponent<Boss_3_Level_Component>() == true)
            {
                Debug.Log("Boss Componenet Found!");
                BossUIComponents.Find(x => x.name == "Boss_Info_3").SetActive(true);
            }
            if (x.GetComponent<Fragment_Key_Componenet>() == true)
            {
                keyFragUI.SetActive(true);
            }

        }

        if(cameraRef.GetCanMove() == false)
        {
            gameplayButtons.Find(x => x.name == "Button Recenter").interactable = false;
        }
       //gameObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;

    }
    public void UpdatePlayerLives(int tmp_)
    {
        switch (tmp_)
        {
            case 3:
                gameplayImages.Find(x => x.name == "Player Health").sprite = playerHealthIcons.Find(x => x.name == "FlameLife_0");
                break;
            case 2:
                gameplayImages.Find(x => x.name == "Player Health").sprite = playerHealthIcons.Find(x => x.name == "FlameLife_1");
                break;
            case 1:
                gameplayImages.Find(x => x.name == "Player Health").sprite = playerHealthIcons.Find(x => x.name == "FlameLife_2");
                break;
            case 0:
                gameplayImages.Find(x => x.name == "Player Health").sprite = playerHealthIcons.Find(x => x.name == "FlameLife_3");
                break;
        }
        //gameplayTexts.Find(x => x.name == "NumberOfLives").text = tmp_.ToString()+"X";
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
