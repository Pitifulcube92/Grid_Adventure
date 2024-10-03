using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Level_Component : Base_Level_Component
{
    [SerializeField] private List<Boss_2_Switch_Script> bossSwitches;
    [SerializeField] private string KeySequence;
    [SerializeField] private string currentSequence;
    [SerializeField] private Transform respawnLocation;
    public override void InitalizeComponent()
    {
        KeySequence = "347158296";
        currentSequence = "";
        GameManager.instance.GetSoundManager().PlayMusicClip("モノクロライブラリー");
        gameObject.GetComponentInParent<Level_Observer>().SetIsKetSpawned(false);
        foreach (Boss_2_Switch_Script x in GameObject.FindObjectsOfType<Boss_2_Switch_Script>())
        {
            bossSwitches.Add(x);
        }
    }

    public override void ResetComponent()
    {
        KeySequence = "347158296";
        currentSequence = "";
        ResetBossSwitches();
    }
    public void compareKey()
    {
       if(KeySequence == currentSequence)
       {
            Debug.Log("Correct sequence has been inputed!");
            gameObject.GetComponentInParent<Level_Observer>().GetLevelObjects().Find(x => x.tag == "Key").gameObject.SetActive(true);
            //Instantiate(floorKey,floorKeyLocation);
       }
       else
       {
            StartCoroutine(ResetSwitches());
            StopCoroutine(ResetSwitches());
          
        }
    }

    public IEnumerator ResetSwitches()
    {       
        yield return StartCoroutine(ClearResults());
        ResetBossSwitches();
    }
    public IEnumerator ClearResults()
    {
        currentSequence = "";
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Tile>().ChangePosition(respawnLocation.position);
        GameManager.instance.GetSoundManager().PlaySFXClip("Retro Event Wrong Echo 03");
        yield return new WaitForSeconds(0.5f);
    }
    public void ResetBossSwitches()
    {
        foreach(Boss_2_Switch_Script x in bossSwitches)
        {
            x.SetIsFlipped(false);
            x.RevertToInitialState();        
        }
    }
    public void EvaluateKey()
    {
        if (CanCompareStatus() == true)
        {
            compareKey();
        }
    }
    public bool CanCompareStatus()
    {
        foreach (Boss_2_Switch_Script x in bossSwitches)
        {
            if (x.GetIsFlipped() == false)
            {
                return false;
            }
        }
        return true;
    }
    public void SendKey(char tmp_)
    {
        currentSequence += tmp_;
    }
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
