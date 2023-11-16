using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Level_Info{
    public bool hasKey { get; set; }
    public bool isLevelDone { get; set; }
    public string levelName { get; set; }
    public int playerLives { get; set; }
    public Transform startPos { get; set; }

    public Level_Info(bool hasKey_, bool isLevelDone_, string levelName_, int playerLives_,Transform startPos_)
    {
        hasKey = hasKey_;
        isLevelDone = isLevelDone_;
        levelName = levelName_;
        playerLives = playerLives_;
        startPos = startPos_;
    }
    public Level_Info(Level_Info tmp_)
    {
        hasKey = tmp_.hasKey;
        isLevelDone = tmp_.isLevelDone;
        levelName = tmp_.levelName;
        playerLives = tmp_.playerLives;
        startPos = tmp_.startPos;
    }
}