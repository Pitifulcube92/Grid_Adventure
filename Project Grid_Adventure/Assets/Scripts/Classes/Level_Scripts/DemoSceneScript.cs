using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.GetSoundManager().PlayMusicClip("Victory_march");
        GameManager.instance.GetUIManager().ChangeUI("Thanks for playing The demo canvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
