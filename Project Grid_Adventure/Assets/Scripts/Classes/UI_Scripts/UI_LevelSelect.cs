using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_LevelSelect : BaseUIScript
{
    [SerializeField] private List<GameObject> WorldPanels;
    [SerializeField] private List<Button> levelBtns;
    [SerializeField] int unlockedLevels;
    [SerializeField] private RawImage backgroundImg;
    [SerializeField] private float y, x;

    void Start()
    {
        foreach(GameObject x in GameObject.FindGameObjectsWithTag("LvlSelectBtn"))
        {
            if (x.GetComponent<Button>())
            {
                levelBtns.Add(x.GetComponent<Button>());
            }
        }

        SetUIConfigure();
    }
    public override void SetUIConfigure()
    {
      foreach (GameObject x in GameObject.FindGameObjectsWithTag("LvlSelectPan"))
      {
            WorldPanels.Add(x);
            Debug.Log(x.name);
      }
      //Detect what levels are available
      //foreach(Button x in levelBtns)
      //{ 
      //  if(levelBtns.IndexOf(x) < unlockedLevels)
      //  {
      //     x.interactable = false;
      //  }
      //}

      foreach(GameObject x in GameObject.FindGameObjectsWithTag("FloorUIbtn"))
      {
            x.SetActive(false);
            switch (x.name)
            {
                case"Floor_btn_1":
                    if (unlockedLevels > 11)
                        x.SetActive(true);
                    break;
                case "Floor_btn_2":
                    if (unlockedLevels > 21)
                        x.SetActive(true);
                    break;
                case "Floor_btn_3":
                    if (unlockedLevels > 31)
                        x.SetActive(true);
                    break;
                case "Floor_btn_4":
                    if (unlockedLevels > 41)
                        x.SetActive(true);
                    break;
                case "Floor_btn_5":
                    if (unlockedLevels == 50)
                        x.SetActive(true);
                    break;
            }           
      }

        SelectWorldPanel(1);
        //gameObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        gameObject.GetComponent<Canvas>().worldCamera = GameObject.FindObjectOfType<Camera>();

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
 
    private void Update()
    {
        backgroundImg.uvRect = new Rect(backgroundImg.uvRect.position + new Vector2(x, y) * Time.deltaTime, backgroundImg.uvRect.size);
    }

}
    