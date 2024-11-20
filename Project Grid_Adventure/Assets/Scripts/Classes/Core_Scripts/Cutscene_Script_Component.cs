using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene_Script_Component : MonoBehaviour
{
    [SerializeField] private string nextScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SkipCutscene();
        }
    }
    public void SkipCutscene()
    {
        if (GameManager.instance.GetGamemode() == Gamemode_State.Cutscene)
        {
            GameManager.instance.GetLevelManager().LoadMainMenu();
        }
        else
        {
            GameManager.instance.GetLevelManager().LoadSceneByName(nextScene);
        }
    }
}
