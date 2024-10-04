using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class UI_MainMenu : BaseUIScript
{
    [SerializeField] private List<Button> menuButtons;
    [SerializeField] private List<Toggle> menuToggles;
    [SerializeField] private List<Slider> menuSliders;
    [SerializeField] private RawImage backgroundImg;

    [SerializeField] private float y, x;
    //[SerializeField] private GameObject GM;
    // Start is called before the first frame update
    void Start()
    {
        //GM = GameObject.FindGameObjectWithTag("Game Manager");
        foreach(Button x in GameObject.FindObjectsOfType<Button>())
        {
            menuButtons.Add(x);
        }
        foreach (Slider x in GameObject.FindObjectsOfType<Slider>())
        {
            menuSliders.Add(x);
        }
        foreach (Toggle x in GameObject.FindObjectsOfType<Toggle>())
        {
            menuToggles.Add(x);
        }
        if (menuButtons == null)
        {
            Debug.LogError("No UI buttons where found!");
        }
      
        SetUIConfigure();
    }
    private void Update()
    {
        backgroundImg.uvRect = new Rect(backgroundImg.uvRect.position + new Vector2(x,y) * Time.deltaTime,backgroundImg.uvRect.size);
    }
    public override void SetUIConfigure()
    {
        foreach (Button x in menuButtons)
        {
            switch (x.gameObject.name)
            {
                case "Start btn":
                    x.onClick.AddListener(delegate { GameManager.instance.GetLevelManager().LoadSceneByName("Level_1_1");
                        GameManager.instance.GetSoundManager().PlayMusicClip("ŒÃ‰®•~‚Å‚Ì”ÓŽ`‰ï“I‚ÈBGM_2");
                    });
                    break;
                case "Continue":
                    break;
                case "Level Select btn":
                    x.onClick.AddListener(delegate { GameManager.instance.GetUIManager().ChangeUI("LevelSelectUI");
                    });
                    break;
                case "Quit btn":
                    x.onClick.AddListener(delegate {Application.Quit();
                        GameManager.instance.GetSoundManager().PlaySFXClip("Retro_Blop_18");
                        //EditorApplication.ExitPlaymode();
                    });
                    break;
            }
            x.onClick.AddListener(delegate { GameManager.instance.GetSoundManager().PlaySFXClip("Retro_Blop_18"); });
        }

        foreach (Toggle x in menuToggles)
        {
            switch (x.gameObject.name)
            {
                case "Fullscreen Toggle":
                    x.isOn = Screen.fullScreen;
                    x.onValueChanged.AddListener(delegate { GameManager.instance.ToggleFullScreen(x.isOn); });
                    break;
            }
        }

        foreach (Slider x in menuSliders)
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

        GameObject.Find("Options Panel").SetActive(false);
        //gameObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        gameObject.GetComponent<Canvas>().worldCamera = GameObject.FindObjectOfType<Camera>();
        //GameObject.FindAnyObjectByType<Camera>();
    }

    
}
