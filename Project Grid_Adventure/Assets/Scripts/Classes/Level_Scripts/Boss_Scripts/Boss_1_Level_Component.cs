using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Level_Component : Base_Level_Component
{
    //TO DO: 
    /*
    Create the following:
    - Timer Func
    - Death Func
    - Boss Health
     */

    [Header("Boss Info")]
    [SerializeField] private int bosshealth;
    [SerializeField] private float countdownSpeed;
    [SerializeField] private float remainingTime;
    //[SerializeField] private GameObject floorKey;
    //[SerializeField] private Transform floorKeyLocation;
    [SerializeField] private GameObject currentChallengeSwitch;
    [SerializeField] private UI_Gameplay uI_Gameplay;

    private float baseInterval;
    private void Awake()
    {
       
    }
    public override void InitalizeComponent()
    {
        bosshealth = 3;
        baseInterval = 1;
        uI_Gameplay = FindObjectOfType<UI_Gameplay>();
       
    }

    public override void ResetComponent()
    {
        bosshealth = 3;
        uI_Gameplay.gameplayTexts.Find(x => x.name == "B1 HP").text = bosshealth.ToString();
        StopAllCoroutines();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.GetSoundManager().PlayMusicClip("Nic8bit");
 
        gameObject.GetComponentInParent<Level_Observer>().SetIsKetSpawned(false);
        uI_Gameplay.gameplayTexts.Find(x => x.name == "B1 HP").text = bosshealth.ToString();
        uI_Gameplay.gameplayTexts.Find(x => x.name == "B1 Timer").text = displayTimer(0);
    }

    // Update is called once per frame

    //Note: when timer is done recreate Timer and set flag to false;
    IEnumerator ChallengeTimer(float time)
    {
        Debug.Log("Challenge start!");
        float waitTime = baseInterval / countdownSpeed;
        while (time > 0)
        {
           yield return new WaitForSeconds(waitTime);        
           time -= countdownSpeed;
           uI_Gameplay.gameplayTexts.Find(x => x.name == "B1 Timer").text = displayTimer(time);
           //Debug.Log(displayTimer(time));
        }
        
        FailedChallenge();
    }
    private void FailedChallenge()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Tile>().NotifyObserver(PlayerState.Taken_Damage);
        currentChallengeSwitch.GetComponent<BaseInteractionTile>().RevertToInitialState();
        currentChallengeSwitch = null;
        StopAllCoroutines();
        uI_Gameplay.gameplayTexts.Find(x => x.name == "B1 Timer").text = displayTimer(0);
    }
    public void DamageBoss()
    {
        bosshealth -= 1;
        uI_Gameplay.gameplayTexts.Find(x => x.name == "B1 HP").text = bosshealth.ToString();
        currentChallengeSwitch.GetComponent<BaseInteractionTile>().RevertToInitialState();
        currentChallengeSwitch = null;
        StopAllCoroutines();
        uI_Gameplay.gameplayTexts.Find(x => x.name == "B1 Timer").text = displayTimer(0);
        if (bosshealth == 0)
        {
            //Instantiate(floorKey, floorKeyLocation);
            gameObject.GetComponentInParent<Level_Observer>().GetLevelObjects().Find(x => x.tag == "Key").gameObject.SetActive(true);
            GameManager.instance.GetSoundManager().PlaySFXClip("Success_Bell",false, GameManager.instance.GetSoundManager().GetSFXSource(1));
        }
    }
    public void StartChallege(float tmp_,GameObject currentswitch_)
    {
        StartCoroutine(ChallengeTimer(tmp_));
        currentChallengeSwitch = currentswitch_;
        currentChallengeSwitch.GetComponent<BoxCollider2D>().enabled = false;
    }
    
    public string displayTimer(float currentTime_)
    {
        float minutes = Mathf.FloorToInt(currentTime_/60);
        float seconds = Mathf.FloorToInt(currentTime_ % 60);
        string result = string.Format("{0:00}:{1:00}", minutes, seconds);
        return result;
    }
}
