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
        /**Get UI and player**/
        scanScene();
        GameManager.instance.GetUIManager().ChangeUI("GameplayUI");
        GameManager.instance.GetSoundManager().PlayMusicClip("DungeonMusic");
        GamePlayUI = GameObject.FindGameObjectWithTag("UICanvas").GetComponent<UI_Gameplay>();      
        watchedSubject = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Tile>();
        fadeCanvas = GameObject.FindGameObjectWithTag("UI").GetComponent<FadeScript>();
        
        /**Get all interactable Tiles**/
        ScanObjectItems();
        isKeySpawned = true;

        /**Initiallize all level Components**/
        foreach (Base_Level_Component x in GameObject.FindObjectsOfType<Base_Level_Component>())
        {
            levelComponents.Add(x);
        }
       
        foreach (Base_Level_Component x in levelComponents)
        {
            x.InitalizeComponent();
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

        //Get subjects
        OnObsEnable();

    }
    public void ChangePlayerSpawnPos(Transform tmp_)
    {
        currentCheckpoint = tmp_.position;
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
    public void SetIsKetSpawned(bool tmp_)
    {
        isKeySpawned = tmp_;
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
                GameManager.instance.GetSoundManager().PlaySFXClip("Retro Success Melody 02 - choir soprano");
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
            case PlayerState.Player_Dead:
                StartCoroutine(Player_InstaKilled());
                break;

        }
    }
    public void ScanObjectItems()
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

    public List<BaseInteractionTile> GetLevelObjects()
    {
        return lvlObjects;
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
    public List<Base_Level_Component> GetLevel_Components()
    {
        return levelComponents;
    }
    //Level Events
    IEnumerator DamagePlayer()
    {
        if (currentLevlInfo.playerLives == 0)
        {
            Debug.Log("Player is dead... Game Over");
            StartCoroutine(GameOver());
            yield return null;
        }
        //level restarts then take player life!
        watchedSubject.GetComponent<Player_Tile>().SetIsMoving(false);
        watchedSubject.GetComponent<SpriteRenderer>().enabled = false;
        currentLevlInfo.playerLives -= 1;
        GamePlayUI.UpdatePlayerLives(currentLevlInfo.playerLives);

        StartCoroutine(Explode());
        //StartCoroutine(Explode());
        yield return new WaitForSeconds(0.7f);
       
        watchedSubject.GetComponent<Player_Tile>().ChangePosition(currentCheckpoint);
        watchedSubject.GetComponent<Player_Tile>().SetIsMoving(true);
        watchedSubject.GetComponent<SpriteRenderer>().enabled = true;
    }
    IEnumerator Explode()
    {
        watchedSubject.GetComponent<Player_Tile>().GetAnimator().Play("Explosion");
        Camera.main.GetComponent<Camera_Shake_Component>().ShakeCamera(0.21f, 0.15f);
        GameManager.instance.GetSoundManager().PlaySFXClip("Death");
        yield return new WaitForSeconds(watchedSubject.GetComponent<Player_Tile>().GetAnimator().GetCurrentAnimatorStateInfo(0).length - 0.01f);
      

    }
    IEnumerator IntroIn()
    {
        watchedSubject.GetComponent<Player_Tile>().SetIsMoving(false);
        yield return StartCoroutine(fadeCanvas.FadeIn());
        watchedSubject.GetComponent<Player_Tile>().SetIsMoving(true);
    }
    IEnumerator Player_InstaKilled()
    {
        StartCoroutine(Camera.main.GetComponent<Camera_Shake_Component>().ShakeCamera(0.21f, 0.15f));
        StartCoroutine(Explode());
        GameManager.instance.GetSoundManager().PlaySFXClip("Death");
        yield return new WaitForSeconds(0.3f);

        StartCoroutine(GameOver());
    }
    IEnumerator GameOver()
    {
        watchedSubject.GetComponent<Player_Tile>().SetIsMoving(false);
        watchedSubject.GetComponent<Player_Tile>().GetAnimator().Play("Explosion");
        Camera.main.GetComponent<Camera_Shake_Component>().ShakeCamera(0.21f, 0.15f);
        GameManager.instance.GetSoundManager().PlaySFXClip("Death");    
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
            GameManager.instance.GetLevelManager().LoadSceneByName("Main Menu");
            //EditorApplication.ExitPlaymode();
            //Application.Quit();
        }
        GameManager.instance.GetLevelManager().LoadSceneByIndex(currentLevlInfo.nextSceneIndex);
        //SceneManager.LoadScene(currentLevlInfo.nextLevelName);
        //EditorApplication.ExitPlaymode();
    }
}
