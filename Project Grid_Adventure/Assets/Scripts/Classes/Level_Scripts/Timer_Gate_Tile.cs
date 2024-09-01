using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer_Gate_Tile : BaseInteractionTile
{
    [Header("Info")]
    [SerializeField] private GameObject targetGate;
    [SerializeField] private float MaxTimer;
    [SerializeField] private float tmpTime;
    [SerializeField] private bool initClosed;
    private bool isClosed;
    // Start is called before the first frame update
    void Start()
    {
        tmpTime = MaxTimer;
        isClosed = initClosed;
        targetGate.SetActive(isClosed);
    }
    // Update is called once per frame
    void Update()
    {
        GateTimer();
    }
    private void GateTimer()
    {
        if (tmpTime >= 0.0f)
        {
            tmpTime -= Time.deltaTime;
        }
        else
        {
            SwitchGate();
            tmpTime = MaxTimer;
        }
    }
    private void SwitchGate()
    {
        if (isClosed)
        {
            targetGate.SetActive(true);
            isClosed = false;
        }
        else
        {
            targetGate.SetActive(false);
            isClosed = true;
        }
    }
    public override void RevertToInitialState()
    {
        isClosed = initClosed;
        targetGate.SetActive(isClosed);
    }
}
