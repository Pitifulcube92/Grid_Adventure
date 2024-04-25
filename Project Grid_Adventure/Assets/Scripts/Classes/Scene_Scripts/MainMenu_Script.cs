using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.GetUIManager().ChangeUI("MenuUI");
    }
}

