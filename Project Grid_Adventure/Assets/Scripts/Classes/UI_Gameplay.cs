using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Gameplay : BaseUIScript
{
    [SerializeField] public Player_Tile target_player;
    [SerializeField] public List<Button> gameplayButtons = new List<Button>();
    [SerializeField] public List<Text> gameplayTexts = new List<Text>();
    //[SerializeField] private List<Image> gameplayImages = new List<Image>();
    [SerializeField] public Level_Observer lvlObs;
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
        foreach(Button x in GameObject.FindObjectsOfType<Button>())
        {
            gameplayButtons.Add(x);
        }
        if (gameplayButtons == null)
        {
            Debug.LogError("No UI buttons where found!");
        }
        foreach (Text x in GameObject.FindObjectsOfType<Text>())
        {
                gameplayTexts.Add(x);
                Debug.Log("Text Name:" + x.name);
        }
        SetUIConfigure();
    }
    public void UpdatePlayerLives(int tmp_)
    {
        
        gameplayTexts.Find(x => x.name == "NumberOfLives").text = tmp_.ToString()+"X";
        Debug.Log("Changed Lives Text!");
        //tmp_.ToString() + "X";
    }
    override public void SetUIConfigure()
    {
        foreach(Button x in gameplayButtons)
        {
            switch (x.gameObject.name)
            {
                //**Click Controls**
                //case "Button Up":
                //    x.onClick.AddListener(delegate { target_player.MovePlayerButton("up");});
                //    break;
                //case "Button Down":
                //    x.onClick.AddListener(delegate { target_player.MovePlayerButton("down"); });
                //    break;
                //case "Button Left":
                //    x.onClick.AddListener(delegate { target_player.MovePlayerButton("left"); });
                //    break;
                //case "Button Right":
                //    x.onClick.AddListener(delegate { target_player.MovePlayerButton("right"); });
                //    break;
                case "Button Reset":
                    x.onClick.AddListener(delegate { return; });
                    break;
            }
        }

        foreach(Text x in gameplayTexts)
        {
            switch (x.gameObject.name)
            {
                case "Level Name":
                    x.text = lvlObs.GetLevel_Info().levelName;
                    break;
                case "NumberOfLives":
                    x.text = lvlObs.GetLevel_Info().playerLives.ToString() + "X";
                    break;
            }
        }

        //foreach (Image x in gameplayImages)
        //{
        //    switch (x.gameObject.name)
        //    {
        //        case "Level Name":
        //            x.text = lvlObs.GetLevel_Info().levelName;
        //            break;
        //    }
        //}
    }
}
