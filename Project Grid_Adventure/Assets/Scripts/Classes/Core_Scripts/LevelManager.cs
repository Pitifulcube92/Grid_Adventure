using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private SceneManager scManager;
    [SerializeField] private List<string> testSceneNames;
    // Start is called before the first frame update
    void Start()
    {
        if (scManager == null)
        {
            scManager = new SceneManager();
        }
        //Debug.Log("Scenes: " + SceneManager.sceneCount);
        ListLevelName();
    }
    
    void GetInitialSceneNames()
    {

    }

    public void LoadScene(string name_)
    {
        SceneManager.LoadScene(name_, LoadSceneMode.Single);
    }
    public void LoadScene(int index_)
    {
        SceneManager.LoadScene(index_, LoadSceneMode.Single);
        GameManager.instance.GetSoundManager().PlayMusicClip("ŒÃ‰®•~‚Å‚Ì”ÓŽ`‰ï“I‚ÈBGM_2");
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");

    }
    public void ListLevelName()
    {

        for (int i = 0; i <= SceneManager.sceneCount; i++)
        {
            //Debug.Log(SceneManager.GetSceneAt(i).name);
        }
    }
}
