using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Gameplay : BaseUIScript
{
    [SerializeField] private Player_Tile target_player;
    [SerializeField] private List<Button> gameplayButtons = new List<Button>();
    // Start is called before the first frame update
    void Start()
    {
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
        //SetUIConfigure();
    }

    override public void SetUIConfigure()
    {
        foreach(Button x in gameplayButtons)
        {
            switch (x.gameObject.name)
            {
                case "Button Up":
                    x.onClick.AddListener(delegate { target_player.MovePlayerButton("up");});
                    break;
                case "Button Down":
                    x.onClick.AddListener(delegate { target_player.MovePlayerButton("down"); });
                    break;
                case "Button Left":
                    x.onClick.AddListener(delegate { target_player.MovePlayerButton("left"); });
                    break;
                case "Button Right":
                    x.onClick.AddListener(delegate { target_player.MovePlayerButton("right"); });
                    break;
            }
        }
    }
}
