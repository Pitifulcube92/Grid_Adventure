using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy_Tile : BaseEnemy_Tile
{
    [SerializeField] public int currentMove;

    // Start is called before the first frame update
    void Start()
    {
        currentMove = 0;

        Debug.Log(CommandPath.Count);
        base.baseStart();
    }

    // Update is called once per frame
    void Update()
    {
       base.BaseUpdate();
    }
    public override void Invokebehavior()
    {
        Debug.Log("Invoked Behavior!");
        //Check if ID is last of the List
        //int tmpLast = CommandPath.FindIndex(CommandPath.Count, x => x.index == currentMove);
        int tmpIndex = CommandPath.Find(x => x.index == currentMove).index;
        int tmpLastIndex = CommandPath.Find(x => x.index == CommandPath.Count - 1).index;
        Debug.Log("Current Move:" + currentMove + "Path index:" + tmpIndex + "Last index:" +tmpLastIndex);
        Debug.Log(currentMove == tmpLastIndex);
        if (currentMove == tmpLastIndex)
        {
            currentMove = 0;
            return;
        }
        Debug.Log(currentMove == tmpIndex);
        //Check is current ID is valid
        if (currentMove == tmpIndex) 
        {
            //Debug.Log(CommandPath.Find(x => x.index == currentMove).Direction);       
            MoveEnemy(CommandPath.Find(x => x.index == currentMove).Direction);
            //currentDirection = CommandPath.Find(x => x.index == currentMove).Direction;

            currentMove++;
        }

       
    }

}
