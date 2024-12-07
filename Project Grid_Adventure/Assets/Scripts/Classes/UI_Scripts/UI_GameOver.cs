using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_GameOver : BaseUIScript
{
    [Header("UI Commonents")]
    [SerializeField] private List<Button> buttons;
    [SerializeField] private Level_Observer lvlObs;
    public override void SetUIConfigure()
    {
       foreach(Button x in buttons)
       {
            switch (x.gameObject.name)
            {
                case "Restart Level":
                    x.onClick.AddListener(delegate { GameManager.instance.GetLevelManager().LoadSceneByName(SceneManager.GetActiveScene().name);
                        GameManager.instance.GetSoundManager().PlaySFXClip("Retro_Blop_18",false, GameManager.instance.GetSoundManager().GetSFXSource(1));
                    });
                    break;
                case "Back to Menu":
                     x.onClick.AddListener(delegate {GameManager.instance.GetLevelManager().LoadSceneByName("Main Menu");
                         GameManager.instance.GetSoundManager().PlaySFXClip("Retro_Blop_18",false, GameManager.instance.GetSoundManager().GetSFXSource(1));
                     });
                    break;
            }
       }
       //gameObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
       gameObject.GetComponent<Canvas>().worldCamera = GameObject.FindAnyObjectByType<Camera>();
       gameObject.GetComponent<Canvas>().sortingLayerName = "UI";
       //gameObject.GetComponent<Canvas>().sortingLayerID = 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!lvlObs)
            lvlObs = GameObject.FindGameObjectWithTag("Level").GetComponent<Level_Observer>();
        
        foreach(Button x in GameObject.FindObjectsOfType<Button>())
        {
            buttons.Add(x);
        }

        SetUIConfigure();
    }
}
