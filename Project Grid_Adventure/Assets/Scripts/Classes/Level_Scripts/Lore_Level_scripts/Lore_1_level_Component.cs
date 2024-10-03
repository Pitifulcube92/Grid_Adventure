using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lore_1_level_Component : Base_Level_Component
{
    public override void InitalizeComponent()
    {
        GameManager.instance.GetSoundManager().PlayMusicClip("Memory_of_stars");
    }
    public override void ResetComponent()
    {
        GameManager.instance.GetSoundManager().PlayMusicClip("Memory_of_stars");
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
