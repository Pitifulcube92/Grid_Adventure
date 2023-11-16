using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Dictionary<string, Canvas> uiCanvas;
    [SerializeField] private Canvas currentCanvas { get; set;}
    // Start is called before the first frame update
    void Start()
    {
        if (!SetupDefaultConfig())
        {
            Debug.LogWarning("An issue has come up with the UI system config");
        }
    }
    
    public bool SetupDefaultConfig()
    {
        if(uiCanvas.Count == 0)
        {
            Debug.LogWarning("No canvas's are provided!");
            return false;
        }
        //TO DO: set current Canvas for main menu
        //ChangeUI(/*Main menu*/);
        return true;
    }

    public void ChangeUI(string name_)
    {
        if (!uiCanvas.ContainsKey(name_))
        {
            Debug.LogError("Canvas was not found!");
        }
        currentCanvas = uiCanvas[name_];

        GameObject.Instantiate(currentCanvas);
    }
}
