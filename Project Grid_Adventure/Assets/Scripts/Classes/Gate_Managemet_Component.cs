using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct GateInfo
{
    public int id;
    public Switch_Tile switchT;
    public GameObject gate;
    public GateInfo(int id_, Switch_Tile switchT_, GameObject gate_) 
    {
        id = id_;
        switchT = switchT_;
        gate = gate_; 
    }
}



public class Gate_Managemet_Component : Base_Level_Component
{
    [Header("Info")]
    [SerializeField] private List<GateInfo> ListOfGates = new List<GateInfo>();
    public override void InitalizeComponent()
    {
        int idCounter = 0;
        //To do: Get all switches and fill Gate info
        foreach(Switch_Tile x in GameObject.FindObjectsOfType<Switch_Tile>())
        {
            ListOfGates.Add(new GateInfo(idCounter, x, x.GetGate()));
            idCounter++;
        }
    }

    public override void ResetComponent()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        base.baseStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
