using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<Canvas> uiCanvas;
    [SerializeField] private Canvas currentCanvas;
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
 
        Destroy(GameObject.FindGameObjectWithTag("UICanvas"));

        foreach(Canvas x in uiCanvas)
        {
            Debug.Log(x.name);
            if(x.gameObject.name == name_)
            {
                if(currentCanvas != null)
                {
                    currentCanvas = null;
                }               
                currentCanvas = x;
                Instantiate(currentCanvas).GetComponent<BaseUIScript>().SetUIConfigure();
               
                return;
            }
        }
    }

    public Canvas GetCurrentUI()
    {
        return currentCanvas;
    }
}
