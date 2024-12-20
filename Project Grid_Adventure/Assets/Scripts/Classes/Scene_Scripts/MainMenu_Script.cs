using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_Script : MonoBehaviour
{
    [SerializeField] private FadeScript fadeIn;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.GetUIManager().ChangeUI("MenuUI");
        GameManager.instance.GetSoundManager().PlayMusicClip("The_Edge_of_a_Dream");
        StopAllCoroutines();
        StartCoroutine(fadeIn.FadeIn());
    }
}

