using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using UnityEngine.SceneManagement;


public class Level_Observer : MonoBehaviour, IObserver
{
    [SerializeField] private ISubject watchedSubject; 
    [SerializeField] private List<BaseInteractionTile> lvlObjects = new List<BaseInteractionTile>();
    [SerializeField] private Level_Info currentLevlInfo;
    [SerializeField] private FadeScript fadeCanvas;

    private void Awake()
    {
        watchedSubject = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Tile>();
        fadeCanvas = GameObject.FindGameObjectWithTag("UI").GetComponent<FadeScript>();
        if (!fadeCanvas)
        {
            Debug.LogWarning("Fade Canvas not found!");
        }
        if (!watchedSubject)
        {
            Debug.LogWarning("subject not found!");
        }
        //Generate level info
        scanScene();
        //Get lvlObjects
        GetObjectItems();
        //Get subject
        OnObsEnable();
        GameManager.instance.GetUIManager().ChangeUI("GameplayUI");
    }
    private void Start()
    {     

        StartCoroutine(IntroIn());

        //Debug.Log("level Name: " + currentLevlInfo.levelName);
    }

    private void OnObsEnable()
    {
        watchedSubject.AddObserver(this);
    }

    private void OnObsDisable()
    {
        watchedSubject.RemoveObserver(this);
    }
    void IObserver.OnNotify(PlayerState action_)
    {
        if (action_ == PlayerState.Interact_Key)
        {
            Debug.Log("Player has taken key");
            currentLevlInfo.hasKey = true;
            GameObject.FindGameObjectWithTag("Key").SetActive(false);
        }
        else if (action_ == PlayerState.Interact_Door)
        {
            
            if(currentLevlInfo.hasKey == true)
            {
                currentLevlInfo.isLevelDone = true;
                GameObject.Destroy(GameObject.FindGameObjectWithTag("Door"));
                Debug.Log("Player has open door!");
                //Debugging 
                StartCoroutine(CompleteLevel());
                //EditorApplication.ExitPlaymode();

            }
            else
            {
                Debug.Log("Player does not have Key!");
            }
            
        }else if (action_ == PlayerState.Taken_Damage)
        {
            if (currentLevlInfo.playerLives == 0)
            {
                
                Debug.Log("Player is dead... Game Over");
                EditorApplication.ExitPlaymode();
                //GameObject.Destroy(GameObject.FindGameObjectWithTag("Player"));
            }
            //level restarts then take player life!

            Debug.Log("Player is hurt back to from start");
            ResetLevelObjects();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Tile>().ChangePosition(currentLevlInfo.startPos);

            currentLevlInfo.isLevelDone = false;
            currentLevlInfo.hasKey = false;
            currentLevlInfo.playerLives -= 1;
            GameManager.instance.GetUIManager().GetCurrentUI().GetComponent<UI_Gameplay>().UpdatePlayerLives(currentLevlInfo.playerLives);
            //GameManager.instance.GetUIManager().GetCurrentUI().GetComponent<UI_Gameplay>().gameplayTexts.Find(x => x.name == "NumberOfLives").text = currentLevlInfo.playerLives.ToString() + "X";
        }
    }
    public void GetObjectItems()
    {
        foreach(BaseInteractionTile x in GameObject.FindObjectsOfType<BaseInteractionTile>())
        {
            lvlObjects.Add(x);
        }
    }
    public void scanScene()
    {
        //Level_Info tmp = new Level_Info();
        currentLevlInfo.levelName = SceneManager.GetActiveScene().name;
        currentLevlInfo.playerLives = 3;
        if (GameObject.FindGameObjectWithTag("Key"))
            currentLevlInfo.hasKey = false;
        if (GameObject.FindGameObjectWithTag("Door"))
            currentLevlInfo.isLevelDone = false;
        if (GameObject.Find("Start Position"))
            currentLevlInfo.startPos = GameObject.Find("Start Position").transform.position;
    }

    public Level_Info GetLevel_Info()
    {
        return currentLevlInfo;
    }
    public GameObject GetKeyItem(string tmp_)
    {
        //Return Item from dictionary
        return new GameObject();
    }

    public void ResetLevelObjects()
    {
        foreach(BaseInteractionTile entry in lvlObjects)
        {
            entry.RevertToInitialState();
        }
    }
    IEnumerator IntroIn()
    {
        watchedSubject.GetComponent<Player_Tile>().SetIsMoving(false);
        yield return StartCoroutine(fadeCanvas.FadeIn());
        watchedSubject.GetComponent<Player_Tile>().SetIsMoving(true);
    }
    IEnumerator CompleteLevel()
    {
        watchedSubject.GetComponent<Player_Tile>().SetIsMoving(false);
        yield return StartCoroutine(fadeCanvas.FadeOut());
        if (currentLevlInfo.nextLevelName.Equals(""))
        {
            EditorApplication.ExitPlaymode();
        }
        SceneManager.LoadScene(currentLevlInfo.nextLevelName);
        //EditorApplication.ExitPlaymode();
    }
}
