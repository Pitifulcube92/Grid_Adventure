using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct GateInfo {
    public int ID;
    public Switch_Tile switches;
    public GameObject Gates;
}



public class Gate_Managemet_Component : Base_Level_Component
{
    [Header("Info")]
    [SerializeField] private List<GateInfo> ListOfGates = new List<GateInfo>();
    public override void InitalizeComponent()
    {
        throw new System.NotImplementedException();
    }

    public override void ResetComponent()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
