using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Intro_Cutscene_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.GetSoundManager().PlayMusicClip("ambience_room");
    }
}
