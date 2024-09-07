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

    [SerializeField] private bool isTimerOn;
    [SerializeField] private float maxTimer;
    [SerializeField] private float timer;
    [Header("Boss Info")]
    [SerializeField] private int bosshealth;
    [SerializeField] private GameObject floorKey;
    [SerializeField] private Transform floorKeyLocation;
    [SerializeField] private GameObject currentChallengeSwitch;
    public override void InitalizeComponent()
    {
        isTimerOn = false;
        bosshealth = 3;
    }

    public override void ResetComponent()
    {
        isTimerOn = false;
        bosshealth = 3;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitalizeComponent();
    }

    // Update is called once per frame
    void Update()
    {
        while (isTimerOn)
        {
            ChallengeTimer();
        }   
    }
    //Note: when timer is done recreate Timer and set flag to false;
    private void ChallengeTimer()
    {
        if (timer >= 0.0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Tile>().NotifyObserver(PlayerState.Taken_Damage);
            isTimerOn = false;
            timer = maxTimer;
        }
    }

    public void DamageBoss()
    {
        bosshealth -= 1;
    }

    public void StartChallege(float tmp_,GameObject currentswitch_)
    {

    }
}
