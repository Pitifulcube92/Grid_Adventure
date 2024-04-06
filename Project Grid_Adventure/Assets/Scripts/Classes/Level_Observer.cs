using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using UnityEngine.SceneManagement;


public class Level_Observer : MonoBehaviour, IObserver
{
    [Header("Primary Info")]
    [SerializeField] private ISubject watchedSubject;
    [SerializeField] private bool isKeySpawned;
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private GameObject doorPrefab;
    [Header("Secondary Info")]
    [SerializeField] private List<BaseInteractionTile> lvlObjects = new List<BaseInteractionTile>();
    [SerializeField] private List<Base_Level_Component> levelComponents = new List<Base_Level_Component>();
    [SerializeField] private UI_Gameplay GamePlayUI;
    [SerializeField] private Level_Info currentLevlInfo;
    [SerializeField] private FadeScript fadeCanvas;

    private void Awake()
    {

        //Get lvlObjects
        scanScene();
        GameManager.instance.GetUIManager().ChangeUI("GameplayUI");
        GamePlayUI = GameObject.FindGameObjectWithTag("UICanvas").GetComponent<UI_Gameplay>();      
        watchedSubject = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Tile>();
        fadeCanvas = GameObject.FindGameObjectWithTag("UI").GetComponent<FadeScript>();
        foreach (Base_Level_Component x in GameObject.FindObjectsOfType<Base_Level_Component>())
        {
            levelComponents.Add(x);
        }
        foreach(Base_Level_Component x in levelComponents)
        {
            x.InitalizeComponent();
        }
        GetObjectItems();
        if (levelComponents.Count != 0)
        {
            levelComponents.Find(x => x.GetComponent<Fragment_Key_Componenet>()).GetComponent<Fragment_Key_Componenet>().SetKey(lvlObjects.Find(x => x.tag == "Key").gameObject);
            lvlObjects.Find(x => x.tag == "Key").GetComponent<KeyTile>().SetInitialActivity(false);
        }
  
        if (!fadeCanvas)
        {
            Debug.LogWarning("Fade Canvas not found!");
        }
        if (!watchedSubject)
        {
            Debug.LogWarning("subject not found!");
        }


        //Get subject
        OnObsEnable();

    }
    private void Start()
    {
       
        if (!isKeySpawned)
            lvlObjects.Find(x => x.tag == "Key").gameObject.SetActive(false);
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
        switch (action_)
        {
            case PlayerState.Interact_Key:
                Debug.Log("Player has taken key");
                currentLevlInfo.hasKey = true;
                GameObject.FindGameObjectWithTag("Key").SetActive(false);
                GameObject.FindGameObjectWithTag("Door").layer = 0;
                GameManager.instance.GetSoundManager().PlaySFXClip("Collect");
                break;

            case PlayerState.Interact_Door:
                if (currentLevlInfo.hasKey == true)
                {
                    currentLevlInfo.isLevelDone = true;
                    GameObject.Destroy(GameObject.FindGameObjectWithTag("Door"));
                    Debug.Log("Player has open door!");
                    GameManager.instance.GetSoundManager().PlaySFXClip("dooropened");
                    //Debugging 
                    StartCoroutine(CompleteLevel());
                    //EditorApplication.ExitPlaymode();
                }
                else
                {
                    Debug.Log("Player does not have Key!");
                }
                break;

            case PlayerState.Taken_Damage:
                if (currentLevlInfo.playerLives == 0)
                {
                    Debug.Log("Player is dead... Game Over");
                    StartCoroutine(GameOver());
                    //EditorApplication.ExitPlaymode();
                    //GameObject.Destroy(GameObject.FindGameObjectWithTag("Player"));
                }

                //level restarts then take player life!
                Debug.Log("Player is hurt back to from start");
                ResetLevelObjects();
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Tile>().ChangePosition(currentLevlInfo.startPos);
                GameManager.instance.GetSoundManager().PlaySFXClip("Death");
                currentLevlInfo.isLevelDone = false;
                currentLevlInfo.hasKey = false;
                currentLevlInfo.playerLives -= 1;
                GamePlayUI.UpdatePlayerLives(currentLevlInfo.playerLives);
                break;

            case PlayerState.Interact_Fragment_Key:
                gameObject.GetComponent<Fragment_Key_Componenet>().GainFragmentKey();
                GameManager.instance.GetSoundManager().PlaySFXClip("Collect");
                break;

        }
    }
    public void GetObjectItems()
    {
        foreach (BaseInteractionTile x in GameObject.FindObjectsOfType<BaseInteractionTile>())
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

        Instantiate(keyPrefab, GameObject.Find("Key Position").transform);
        Instantiate(doorPrefab, GameObject.Find("Door Position").transform);
    }

    public Level_Info GetLevel_Info()
    {
        return currentLevlInfo;
    }

    public void ResetLevelObjects()
    {
        foreach(BaseInteractionTile entry in lvlObjects)
        {
            entry.RevertToInitialState();
        }
       foreach(Base_Level_Component comp in levelComponents)
       {
            comp.ResetComponent();
       }
    }
    IEnumerator IntroIn()
    {
        watchedSubject.GetComponent<Player_Tile>().SetIsMoving(false);
        yield return StartCoroutine(fadeCanvas.FadeIn());
        watchedSubject.GetComponent<Player_Tile>().SetIsMoving(true);
    }
    IEnumerator GameOver()
    {
        //watchedSubject.GetComponent<Player_Tile>().SetIsMoving(false);
        yield return StartCoroutine(fadeCanvas.FadeOut());
        GameManager.instance.GetLevelManager().LoadScene("Main Menu");
    }
    IEnumerator CompleteLevel()
    {
        watchedSubject.GetComponent<Player_Tile>().SetIsMoving(false);
        yield return StartCoroutine(fadeCanvas.FadeOut());
        if (currentLevlInfo.nextLevelName.Equals(""))
        {
            //EditorApplication.ExitPlaymode();
            Application.Quit();
        }
        SceneManager.LoadScene(currentLevlInfo.nextLevelName);
        //EditorApplication.ExitPlaymode();
    }
}
