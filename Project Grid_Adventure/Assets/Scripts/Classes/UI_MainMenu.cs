using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : BaseUIScript
{
    [SerializeField] private List<Button> menuButtons;
    [SerializeField] private GameObject GM;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("Game Manager");
        foreach(Button x in GameObject.FindObjectsOfType<Button>())
        {
            menuButtons.Add(x);
        }
        if(menuButtons == null)
        {
            Debug.LogError("No UI buttons where found!");
        }
        SetUIConfigure();
    }

    public override void SetUIConfigure()
    {
        foreach (Button x in menuButtons)
        {
            switch (x.gameObject.name)
            {
                case "Start btn":
                    x.onClick.AddListener(delegate { GameManager.instance.GetLevelManager().LoadScene("Test_Level_1");});
                    break;
                case "Level Select btn":
                    x.onClick.AddListener(delegate { GameManager.instance.GetUIManager().ChangeUI("Canvas_Template(Level select)");});
                    break;
                case "Options":
                    break;
            }


        }
    }
}
