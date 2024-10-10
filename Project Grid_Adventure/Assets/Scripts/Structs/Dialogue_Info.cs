using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public struct Dialogue_Info 
{
    public string name;
    [TextArea(3,10)]
    public string[] sentences;
    public bool hasImage;
    public Sprite targetImage;
    public Vector2 imageDimension;
}
