using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Base_Level_Component : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InitalizeComponent();
    }
    public void baseStart()
    {
        Start();
    }
    public abstract void InitalizeComponent();
    public abstract void ResetComponent();
}
