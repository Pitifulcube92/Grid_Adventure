using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TurretDirection { 
    DIR_UP,
    DIR_DOWN,
    DIR_RIGHT,
    DIR_LEFT
}

public class Turret_TIle : BaseInteractionTile
{
    [Header("Info")]
    [SerializeField] private Transform launchLocation;
    [SerializeField] private GameObject projectial;
    [SerializeField] private bool canInitialFire;

    [SerializeField, Range(0,10)] private float turretSpeed;
    [Header("Timer Info")]
    [SerializeField] float maxTimer;
    [SerializeField] float tmpTime;
    // Start is called before the first frame update
    void Start()
    {
        tmpTime = maxTimer;
        if(canInitialFire == true)
        {
            FireTurrent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        firingTimer();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(gameObject.transform.forward, gameObject.transform.forward * 20f);
    }
    private void OnDrawGizmos()
    {
       
    }
    private void firingTimer()
    {
        if(tmpTime >= 0.0f)
        {
            tmpTime -= Time.deltaTime;
        }
        else
        {
            FireTurrent();
            tmpTime = maxTimer;
        }
    }
    private void FireTurrent()
    {
        GameObject tmp = Instantiate(projectial, launchLocation);
        tmp.GetComponent<Fire_Canonball>().SetSpeed(turretSpeed);
    }
    [ContextMenu("Rotate Left")]
    private void RotateCClockwise()
    {
        gameObject.transform.Rotate(0,0,90,Space.Self);
    }
    [ContextMenu("Rotate Right")]
    private void RotateClockwise()
    {
        gameObject.transform.Rotate(0, 0, -90, Space.Self);
    }
    public override void RevertToInitialState()
    {
        Start();
    }
}
