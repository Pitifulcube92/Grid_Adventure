using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opening_Cutscene_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.GetSoundManager().PlayMusicClip("Al_Fine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
