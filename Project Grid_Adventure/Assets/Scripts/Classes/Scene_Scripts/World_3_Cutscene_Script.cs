using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_3_Cutscene_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.GetSoundManager().PlayMusicClip("Creepy_BGM");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
