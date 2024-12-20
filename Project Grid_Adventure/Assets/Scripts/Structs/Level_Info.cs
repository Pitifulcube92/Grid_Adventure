using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public struct Level_Info
{
    public bool hasKey;
    public bool isLevelDone;
    public string levelName;
    public int nextSceneIndex;
    public int playerLivesMax;
    public int currentPlayerLives;
    public Vector3 startPos;

    public Level_Info(bool hasKey_, bool isLevelDone_, string levelName_, int playerLivesMax_, Vector3 startPos_, int nextSceneIndex_)
    {
        hasKey = hasKey_;
        isLevelDone = isLevelDone_;
        levelName = levelName_;
        playerLivesMax = playerLivesMax_;
        currentPlayerLives = playerLivesMax;
        nextSceneIndex = nextSceneIndex_;
        startPos = startPos_;
    }
    public Level_Info(Level_Info tmp_)
    {
        hasKey = tmp_.hasKey;
        isLevelDone = tmp_.isLevelDone;
        levelName = tmp_.levelName;
        playerLivesMax = tmp_.playerLivesMax;
        currentPlayerLives = tmp_.currentPlayerLives;
        startPos = tmp_.startPos;
        nextSceneIndex = tmp_.nextSceneIndex;
    }
}