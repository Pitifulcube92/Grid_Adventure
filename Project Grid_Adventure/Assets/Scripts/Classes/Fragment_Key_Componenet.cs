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
    void Start()
    {
        if(fragmentLocations.Count == 0)
        {
            foreach(GameObject x in GameObject.FindGameObjectsWithTag("Fragment Location"))
            {
                fragmentLocations.Add(x.transform);
            }
        }
    }

    private void CheckFragmentRequirement()
    {
        if(MaxFragments == currentFragments)
        {
            Instantiate(mainKey, GameObject.Find("Key Position").transform);
        }
    }
    public void GainFragmentKey()
    {
        currentFragments++;
        CheckFragmentRequirement();
    }
    public override void InitalizeComponent()
    {
        foreach (Transform x in fragmentLocations)
        {
            Instantiate(fragmentKey, x);
        }
    }

    public override void ResetComponent()
    {
        currentFragments = 0;
    }
}
