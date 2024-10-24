using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lore_1_level_Component : Base_Level_Component
{
    public override void InitalizeComponent()
    {
        GameManager.instance.GetSoundManager().PlayMusicClip("Remnants_of_summer_2");
    }
    public override void ResetComponent()
    {
        GameManager.instance.GetSoundManager().PlayMusicClip("Remnants_of_summer_2");
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
