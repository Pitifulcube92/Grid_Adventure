using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lore_2_level_Component : Base_Level_Component
{
    public override void InitalizeComponent()
    {
        GameManager.instance.GetSoundManager().PlayMusicClip("on_a_quiet_night");
    }
    public override void ResetComponent()
    {
        GameManager.instance.GetSoundManager().PlayMusicClip("on_a_quiet_night");
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
