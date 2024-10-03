using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fragment_Key_Componenet : Base_Level_Component
{

    [Header("Info")]
    [SerializeField] private int MaxFragments;
    [SerializeField] private int currentFragments;
    [SerializeField] private List<Transform> fragmentLocations;
    [SerializeField] private GameObject mainKey;
    [SerializeField] private GameObject fragmentKey;
    // Start is called before the first frame update

    private void Awake()
    {
      
    }
    void Start()
    {
        //base.baseStart();
    }

    public void CheckFragmentRequirement()
    {
        if(MaxFragments == currentFragments)
        {
            //mainKey.SetActive(true);
            gameObject.GetComponentInParent<Level_Observer>().GetLevelObjects().Find(x => x.tag == "Key").GetComponent<KeyTile>().SetInitialActivity(true);
            GameManager.instance.GetSoundManager().SetSFXVolume(0.5f);
            GameManager.instance.GetSoundManager().PlaySFXClip("Success_Bell");
            GameManager.instance.GetSoundManager().SetSFXVolume(GameManager.instance.GetSoundManager().GetBGMVolume());
        }
    }
    public void GainFragmentKey()
    {
        currentFragments++;
        CheckFragmentRequirement();
    }
    public override void InitalizeComponent()
    {
        if (fragmentLocations.Count == 0)
        {
            foreach (GameObject x in GameObject.FindGameObjectsWithTag("Fragment Location"))
            {
                fragmentLocations.Add(x.transform);
            }
            MaxFragments = GameObject.FindGameObjectsWithTag("Fragment Location").Length;
        }
        foreach (Transform x in fragmentLocations)
        {
            Instantiate(fragmentKey, x);
        }
        //mainKey = gameObject.GetComponentInParent<Level_Observer>().GetLevelObjects().Find(x => x.tag == "Key").GetComponent<GameObject>();
        gameObject.GetComponentInParent<Level_Observer>().GetLevelObjects().Find(x => x.tag == "Key").GetComponent<KeyTile>().SetInitialActivity(false);

    }
    public void SetKey(GameObject tmp_)
    {
        mainKey = tmp_;
    }
    public override void ResetComponent()
    {
        currentFragments = 0;
    }
}
