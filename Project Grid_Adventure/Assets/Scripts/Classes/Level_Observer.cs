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

    private void Start()
    {
        //Generate level info
        currentLevlInfo = scanScene();
        //Get lvlObjects
        GetObjectItems();
        //Get subject
        watchedSubject = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Tile>();
        if (!watchedSubject)
        {
            Debug.LogWarning("subject not found!");
        }
        OnObsEnable();
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
                EditorApplication.ExitPlaymode();
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
            currentLevlInfo.playerLives -= 1;
        }
    }
    public void GetObjectItems()
    {
        foreach(BaseInteractionTile x in GameObject.FindObjectsOfType<BaseInteractionTile>())
        {
            lvlObjects.Add(x);
        }
    }
    public Level_Info scanScene()
    {
        Level_Info tmp = new Level_Info();
        tmp.levelName = SceneManager.GetActiveScene().name;
        tmp.playerLives = 3;
        if (GameObject.FindGameObjectWithTag("Key"))
            tmp.hasKey = false;
        if (GameObject.FindGameObjectWithTag("Door"))
            tmp.isLevelDone = false;
        if (GameObject.Find("PlayerPos"))
            tmp.startPos = GameObject.Find("PlayerPos").transform;
        return tmp;
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
}
