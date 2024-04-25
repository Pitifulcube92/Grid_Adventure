using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_LevelSelect : MonoBehaviour
{
    [SerializeField] private GameObject lvlContainer;
    [SerializeField] private Text worldName;
    [SerializeField] private List<Button> levelBtns;
    //[SerializeField] private List<string> lvlNames;
    [SerializeField] private Button btnPrefab;
    [SerializeField] private int numOfBtns;
    [SerializeField] private string currentWorldName;
    

    void Start()
    { 
      worldName.text = "World 1";

      for (int i =0; i < numOfBtns; i++)
      {
           btnPrefab.GetComponentInChildren<Text>().text = "LVL " + (i+1);
           btnPrefab.GetComponent<SceneData>().sceneName = currentWorldName + (i + 1);
           levelBtns.Add(Instantiate(btnPrefab, lvlContainer.transform));
      }
    }

    
}
    