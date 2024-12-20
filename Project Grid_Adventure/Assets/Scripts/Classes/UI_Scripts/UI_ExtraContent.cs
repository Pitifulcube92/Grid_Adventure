using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_ExtraContent : BaseUIScript
{
    [SerializeField] private RawImage backgroundImg;
    [SerializeField] private List<Button> ContentBtns;
    [SerializeField] private Button backBtn;
    [SerializeField] private float y, x;

    public override void SetUIConfigure()
    {
        GameObject.Find("Back btn").GetComponent<Button>();
        backBtn.onClick.AddListener(delegate { GameManager.instance.GetSoundManager().PlaySFXClip("Retro_Blop_18",false, GameManager.instance.GetSoundManager().GetSFXSource(1)); });
        gameObject.GetComponent<Canvas>().worldCamera = GameObject.FindObjectOfType<Camera>();
       
        foreach (Button x in ContentBtns)
        {
            x.onClick.AddListener(delegate { GameManager.instance.SetGamemode(2); });
        }
        foreach (Button x in ContentBtns)
        {
            switch (x.name)
            {
                case "Boss_1_btn":
                    if (GameManager.instance.GetCurrentLevel() <= 20)
                    {
                        x.GetComponentInChildren<Text>().text = "????";
                        x.interactable = false;
                    }                       
                    break;
                case "Boss_2_btn":
                    if (GameManager.instance.GetCurrentLevel() <= 30)
                    {
                        x.GetComponentInChildren<Text>().text = "????";
                        x.interactable = false;
                    }
                    break;
                case "Boss_3_btn":
                    if (GameManager.instance.GetCurrentLevel() <= 40)
                    {
                        x.GetComponentInChildren<Text>().text = "????";
                        x.interactable = false; 
                    }
                    break;
                case "Ending_btn":
                    if (GameManager.instance.GetCurrentLevel() != 50)
                    {
                        x.GetComponentInChildren<Text>().text = "????";
                        x.interactable = false;
                    }
                    break;
            }
        }

      
    }
        // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Canvas>().worldCamera = GameObject.FindObjectOfType<Camera>();
        foreach(GameObject x in GameObject.FindGameObjectsWithTag("CutScenebtn"))
        {
            ContentBtns.Add(x.GetComponent<Button>());
        }
        SetUIConfigure();
    }

    // Update is called once per frame
    private void Update()
    {
        backgroundImg.uvRect = new Rect(backgroundImg.uvRect.position + new Vector2(x, y) * Time.deltaTime, backgroundImg.uvRect.size);
    }
}
