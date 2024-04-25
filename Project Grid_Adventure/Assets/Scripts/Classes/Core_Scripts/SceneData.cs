using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[System.Serializable]
public class SceneData : MonoBehaviour
{
    public int sceneIndex;
    public string sceneName;
    public Button btn;
    public LevelManager lvlMng;
    private void Start()
    {
        if (btn == null)
            btn = gameObject.GetComponent<Button>();
        if (lvlMng == null)
            lvlMng = GameObject.FindAnyObjectByType<LevelManager>();
        btn.onClick.AddListener(delegate { lvlMng.LoadScene(sceneName); });
        Debug.Log("Scene Name: " + sceneName);
    }
}
