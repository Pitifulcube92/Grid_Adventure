using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class UI_MainMenu : BaseUIScript
{
    [SerializeField] public List<Button> menuButtons;
    //[SerializeField] private GameObject GM;
    // Start is called before the first frame update
    void Start()
    {
        //GM = GameObject.FindGameObjectWithTag("Game Manager");
        foreach(Button x in GameObject.FindObjectsOfType<Button>())
        {
            menuButtons.Add(x);
        }
        if(menuButtons == null)
        {
            Debug.LogError("No UI buttons where found!");
        }
        GameManager.instance.GetSoundManager().PlayMusicClip("Dazzling_2");
        SetUIConfigure();
    }

    public override void SetUIConfigure()
    {
        foreach (Button x in menuButtons)
        {
            switch (x.gameObject.name)
            {
                case "Start btn":
                    x.onClick.AddListener(delegate { GameManager.instance.GetLevelManager().LoadScene("Test_Level_1");
                        GameManager.instance.GetSoundManager().PlayMusicClip("ŒÃ‰®•~‚Å‚Ì”ÓŽ`‰ï“I‚ÈBGM_2");});
                    break;
                case "Continue":
                    break;
                case "Level Select btn":
                    //x.onClick.AddListener(delegate { GameManager.instance.GetUIManager().ChangeUI("Canvas_Template(Level select)",GameManager.instance.transform);});
                    break;
                case "Options":
                    break;
                case "Quit btn":
                    x.onClick.AddListener(delegate {Application.Quit();
                                                    //EditorApplication.ExitPlaymode();
                                                    });
                    break;
            }
        }
    }

    
}
