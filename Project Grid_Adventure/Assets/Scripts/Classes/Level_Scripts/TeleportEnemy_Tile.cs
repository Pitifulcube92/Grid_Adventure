using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct EnemyMoveInfo
{
    public int index;
    public Transform positionInfo;

    public EnemyMoveInfo(int index_, Transform position_)
    {
        index = index_;
        positionInfo = position_;
    }   
}

public class TeleportEnemy_Tile : BaseEnemy_Tile
{
    [SerializeField] List<EnemyMoveInfo> enemyPaths = new List<EnemyMoveInfo>();
    [SerializeField] private int maxIndex;
    // Start is called before the first frame update
    void Start()
    { 
       
        //Debug.Log(CommandPath.Count);
        int tmpIndex = 0;
        //gameObject.chil
        foreach(Transform x in gameObject.GetComponentInChildren<Transform>())
        {
            if (x.tag == "enemyPos")
            {
                enemyPaths.Add(new EnemyMoveInfo(tmpIndex, x));
                tmpIndex++;
            }
        }
        maxIndex = enemyPaths.Count;
        foreach (EnemyMoveInfo x in enemyPaths)
        {
            x.positionInfo.parent = null;
        }
        Invokebehavior();
        base.baseStart();
    }

    // Update is called once per frame
    void Update()
    {
        base.BaseUpdate();
    }

    private int GenerateRandomIndex()
    {
        int tmpRng = (int)UnityEngine.Random.Range(0f,maxIndex - 1);
        
        return tmpRng;
    }
    public override void Invokebehavior()
    {
        //Move Character from a random position
        int tmp = GenerateRandomIndex();
        //Debug.Log("Index Next: " + tmp);
        ChangePosition(enemyPaths.Find(x => x.index == tmp).positionInfo.position);
    }
}
