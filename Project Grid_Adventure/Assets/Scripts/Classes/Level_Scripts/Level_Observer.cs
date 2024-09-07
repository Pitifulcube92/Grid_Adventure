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
    [SerializeField] private Vector3 currentCheckpoint;
    //[SerializeField] private 
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
        if (levelComponents.Count > 0)
        {
            if (levelComponents.Exists(x => x.GetComponent<Fragment_Key_Componenet>()))
            {
                levelComponents.Find(x => x.GetComponent<Fragment_Key_Componenet>()).GetComponent<Fragment_Key_Componenet>().SetKey(lvlObjects.Find(x => x.tag == "Key").gameObject);
                lvlObjects.Find(x => x.tag == "Key").GetComponent<KeyTile>().SetInitialActivity(false);
                isKeySpawned = false;
            }
            else
            {
                isKeySpawned = true;
            }
        }
        
      
        if (!fadeCanvas)
        {
            //fadeCanvas = GameObject.Find("FadeCanvas").GetComponent<FadeScript>();
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
        {
            lvlObjects.Find(x => x.tag == "Key").gameObject.SetActive(false);
        }

        currentCheckpoint = currentLevlInfo.startPos;
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
                //Debug.Log("Player has taken key");
                currentLevlInfo.hasKey = true;
                lvlObjects.Find(x => x.tag == "Key").gameObject.SetActive(false);
                lvlObjects.Find(x => x.tag == "Door").gameObject.layer = 0;
                GameManager.instance.GetSoundManager().PlaySFXClip("Collect");
                break;

            case PlayerState.Interact_Door:
                if (currentLevlInfo.hasKey == true)
                {
                    currentLevlInfo.isLevelDone = true;
                    GameManager.instance.GetSoundManager().PlaySFXClip("dooropened");
                    //Debugging 
                    StartCoroutine(CompleteLevel());
                }
                else
                {
                    Debug.Log("Player does not have Key!");
                }
                break;

            case PlayerState.Taken_Damage:
                StartCoroutine(DamagePlayer());
                break;

            case PlayerState.Interact_Checkpoint:
                //Call in UI
                currentCheckpoint = GetInteractedTile().transform.position;
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

    public void RestFromCheckpoint()
    {
        lvlObjects.Find(x => x.tag == "Key").RevertToInitialState();
        if(gameObject.GetComponent<Fragment_Key_Componenet>())
        {
            gameObject.GetComponent<Fragment_Key_Componenet>().CheckFragmentRequirement();
        }
    }

    private BaseInteractionTile GetInteractedTile()
    {
        foreach(BaseInteractionTile x in lvlObjects)
        {
            if (Physics2D.IsTouching(x.GetComponent<Collider2D>(), watchedSubject.GetComponent<Collider2D>()))
            {
                Debug.Log("Returned " + x.name);
                return x;
            }
        }
        return null;
    }
    //Level Events
    IEnumerator DamagePlayer()
    {
        if (currentLevlInfo.playerLives == 0)
        {
            Debug.Log("Player is dead... Game Over");
            StartCoroutine(GameOver());
        }
        //level restarts then take player life!

        currentLevlInfo.isLevelDone = false;
        //currentLevlInfo.hasKey = false;
        currentLevlInfo.playerLives -= 1;
        GamePlayUI.UpdatePlayerLives(currentLevlInfo.playerLives);

        yield return StartCoroutine(Camera.main.GetComponent<Camera_Shake_Component>().ShakeCamera(0.1f,0.15f));
        
        watchedSubject.GetComponent<Player_Tile>().ChangePosition(currentCheckpoint);
        GameManager.instance.GetSoundManager().PlaySFXClip("Death");

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
        GameManager.instance.GetUIManager().ChangeUI("GameOverUI");
        //GameManager.instance.GetLevelManager().LoadScene("Main Menu");
    }
    IEnumerator CompleteLevel()
    {
        watchedSubject.GetComponent<Player_Tile>().SetIsMoving(false);
        yield return StartCoroutine(fadeCanvas.FadeOut());
        if (currentLevlInfo.nextSceneIndex.Equals(0))
        {
            GameManager.instance.GetLevelManager().LoadScene("Main Menu");
            //EditorApplication.ExitPlaymode();
            //Application.Quit();
        }
        GameManager.instance.GetLevelManager().LoadScene(currentLevlInfo.nextSceneIndex);
        //SceneManager.LoadScene(currentLevlInfo.nextLevelName);
        //EditorApplication.ExitPlaymode();
    }
}
