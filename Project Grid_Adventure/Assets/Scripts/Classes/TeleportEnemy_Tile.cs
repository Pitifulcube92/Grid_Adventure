using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct EnemyMoveInfo
{
    public int index;
    public Transform positionInfo;
}

public class TeleportEnemy_Tile : BaseEnemy_Tile
{
    [SerializeField] List<EnemyMoveInfo> enemyPaths;
    [SerializeField] private int maxIndex;
    // Start is called before the first frame update
    void Start()
    { 
        maxIndex = enemyPaths.Count;
        //Debug.Log(CommandPath.Count);
        foreach(EnemyMoveInfo x in enemyPaths)
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
        Debug.Log("Index Next: " + tmp);
        ChangePosition(enemyPaths.Find(x => x.index == tmp).positionInfo.position);
    }
}
