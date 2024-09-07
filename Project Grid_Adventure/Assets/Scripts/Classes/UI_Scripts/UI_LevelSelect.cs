using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_LevelSelect : BaseUIScript
{
    [SerializeField] private List<GameObject> WorldPanels;
    [SerializeField] private RawImage backgroundImg;
    [SerializeField] private float y, x;

    public override void SetUIConfigure()
    {
      foreach (GameObject x in GameObject.FindGameObjectsWithTag("LvlSelectPan"))
      {
            WorldPanels.Add(x);
            Debug.Log(x.name);
      }
        SelectWorldPanel(1);
        
    }
    public void SelectWorldPanel(int tmp_)
    {
        switch (tmp_)
        {
            case 1:
                // WorldPanels.Find(x => x.name.Equals("World 1 btns"));
                foreach (GameObject x in WorldPanels)
                {
                    x.SetActive(false);

                    if (x.name == "World 1 btns")
                    {
                        x.SetActive(true);
                    }          
                }         
                break;
            case 2:
                foreach (GameObject x in WorldPanels)
                {
                    x.SetActive(false);

                    if (x.name == "World 2 btns")
                    {
                        x.SetActive(true);
                    }
                }
                break;
            case 3:
                foreach (GameObject x in WorldPanels)
                {
                    x.SetActive(false);

                    if (x.name == "World 3 btns")
                    {
                        x.SetActive(true);
                    }                  
                }
                break;
            case 4:
                foreach (GameObject x in WorldPanels)
                {
                    x.SetActive(false);

                    if (x.name == "World 4 btns")
                    {
                        x.SetActive(true);
                    }                   
                }
                break;
            case 5:
                foreach (GameObject x in WorldPanels)
                {
                    x.SetActive(false);

                    if (x.name == "World 5 btns")
                    {
                        x.SetActive(true);
                    }                  
                }
                break;
        }
    }
    void Start()
    {
        //SetUIConfigure();
    }
    private void Update()
    {
        backgroundImg.uvRect = new Rect(backgroundImg.uvRect.position + new Vector2(x, y) * Time.deltaTime, backgroundImg.uvRect.size);
    }

}
    