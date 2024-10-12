using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_3_Level_Component : Base_Level_Component
{
    [SerializeField] private UI_Gameplay uI_Gameplay;
    //[SerializeField] private float TrailTime;
    [SerializeField] private float countdownSpeed;
    [SerializeField] private float remainingTime;
    [SerializeField] private GameObject currentChallengeSwitch;
    private float baseInterval;
    public override void InitalizeComponent()
    {
        baseInterval = 1;
        uI_Gameplay = FindObjectOfType<UI_Gameplay>();
    }

    public override void ResetComponent()
    {
        StopAllCoroutines();
    }
    IEnumerator ChallengeTimer(float time)
    {
        Debug.Log("Challenge start!");
        float waitTime = baseInterval / countdownSpeed;
        while (time > 0)
        {
            yield return new WaitForSeconds(waitTime);
            time -= countdownSpeed;
            uI_Gameplay.gameplayTexts.Find(x => x.name == "B3_Timer_left").text = displayTimer(time);
            //Debug.Log(displayTimer(time));
        }

        FailedChallenge();
    }
    private void FailedChallenge()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Tile>().NotifyObserver(PlayerState.Player_Dead);
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Tile>().NotifyObserver(PlayerState.);

    }

    public string displayTimer(float currentTime_)
    {
        float minutes = Mathf.FloorToInt(currentTime_ / 60);
        float seconds = Mathf.FloorToInt(currentTime_ % 60);
        string result = string.Format("{0:00}:{1:00}", minutes, seconds);
        return result;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.GetSoundManager().PlayMusicClip("モノクロライブラリー");
        uI_Gameplay.gameplayTexts.Find(x => x.name == "B3_Timer_left").text = displayTimer(0);
    }
    public void StartChallege(float tmp_, GameObject currentswitch_)
    {
        StartCoroutine(ChallengeTimer(tmp_));
        currentChallengeSwitch = currentswitch_;
        currentChallengeSwitch.GetComponent<BoxCollider2D>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
