using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Credit_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   
        GameManager.instance.GetSoundManager().PlayMusicClip("スターもありがとナイスタ！");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
