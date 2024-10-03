using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Base_Level_Component : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        //InitalizeComponent();
    }
    public void baseStart()
    {
        Awake();
    }
    public abstract void InitalizeComponent();
    public abstract void ResetComponent();
}
