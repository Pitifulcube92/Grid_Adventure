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
    }

    // Update is called once per frame
    void Update()
    {
       base.BaseUpdate();
    }
    public override void Invokebehavior()
    {
        Debug.Log("Invoked Behavior!");
        //Check is current ID is valid
        if (CommandPath.Find(x => x.index == currentMove).index == currentMove) 
        {
            Debug.Log(CommandPath.Find(x => x.index == currentMove).Direction);
            
            MoveEnemy(CommandPath.Find(x => x.index == currentMove).Direction);
           
        }
        //Check if ID is last of the List
        int tmpLast = CommandPath.FindIndex(CommandPath.Count, x => x.index == currentMove);
        if (CommandPath.FindLast(x => x.index == currentMove).index == currentMove)
        {
            currentMove = 0;
            CommandPath
        }
        currentMove++;
    }

}
