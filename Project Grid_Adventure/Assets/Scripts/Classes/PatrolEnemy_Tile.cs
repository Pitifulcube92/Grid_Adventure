using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct EnemyMoveCommand
{
    public int index;
    public EnemyDirection Direction;
}
public enum EnemyDirection
{
    ED_UP,
    ED_DOWN,
    ED_LEFT,
    ED_RIGHT,
    ED_NONE
}
public class PatrolEnemy_Tile : BaseEnemy_Tile
{
    [SerializeField] public int currentMove;
    [SerializeField] protected float speed;
    [SerializeField] protected float movePointDistance;
    [SerializeField] protected List<EnemyMoveCommand> CommandPath;
    [SerializeField] protected EnemyDirection currentDirection;
    // Start is called before the first frame update
    void Start()
    {
        currentMove = CommandPath.Find(x => x.index == 0).index;
        currentDirection = CommandPath.Find(x => x.index == 0).Direction;
        //Debug.Log(CommandPath.Count);
        Invokebehavior();
        base.baseStart();
        StopCoroutine("MoveToTarget");
        StartCoroutine(MoveToTarget());

    }

    // Update is called once per frame
    void Update()
    {
       base.BaseUpdate();
    }
    public void MoveEnemy(EnemyDirection tmp_)
    {
        if (Vector3.Distance(transform.position, enemyMovePoint.position) <= 0.5f && transform.position == enemyMovePoint.position)
        {
            if (tmp_ == EnemyDirection.ED_RIGHT)
            {
                if (!Physics2D.OverlapCircle(enemyMovePoint.position + new Vector3(movePointDistance, 0f, 0f), 0.2f, unWalkable))
                {
                    enemyMovePoint.position += new Vector3(movePointDistance, 0f, 0f);
                    //ChangePosition(enemyMovePoint.position + new Vector3(movePointDistance, 0f, 0f));
                    StartCoroutine(MoveToTarget());
                }

                //transform.position = Vector3.MoveTowards(transform.position, enemyMovePoint.position, speed * Time.deltaTime);
            }
            else if (tmp_ == EnemyDirection.ED_LEFT)
            {
                if (!Physics2D.OverlapCircle(enemyMovePoint.position + new Vector3(-movePointDistance, 0f, 0f), 0.2f, unWalkable))
                {
                    enemyMovePoint.position += new Vector3(-movePointDistance, 0f, 0f);
                    //ChangePosition(enemyMovePoint.position + new Vector3(-movePointDistance, 0f, 0f));
                    StartCoroutine(MoveToTarget());
                }

                //transform.position = Vector3.MoveTowards(transform.position, enemyMovePoint.position, speed * Time.deltaTime);
            }
            else if (tmp_ == EnemyDirection.ED_UP)
            {
                if (!Physics2D.OverlapCircle(enemyMovePoint.position + new Vector3(0f, movePointDistance, 0f), 0.2f, unWalkable))
                {
                    enemyMovePoint.position += new Vector3(0f, movePointDistance, 0f);
                    //ChangePosition(enemyMovePoint.position + new Vector3(0f, movePointDistance, 0f));
                    StartCoroutine(MoveToTarget());
                }

                //transform.position = Vector3.MoveTowards(transform.position, enemyMovePoint.position, speed * Time.deltaTime);
            }
            else if (tmp_ == EnemyDirection.ED_DOWN)
            {
                if (!Physics2D.OverlapCircle(enemyMovePoint.position + new Vector3(0f, -movePointDistance, 0f), 0.2f, unWalkable))
                {
                    enemyMovePoint.position += new Vector3(0f, -movePointDistance, 0f);
                    //ChangePosition(enemyMovePoint.position + new Vector3(0f, -movePointDistance, 0f));
                    StartCoroutine(MoveToTarget());
                    //transform.position = Vector3.MoveTowards(transform.position, enemyMovePoint, smoothing * Time.deltaTime);
                }

                //transform.position = Vector3.MoveTowards(transform.position, enemyMovePoint.position, speed * Time.deltaTime);
            }


        }
    }

    public override void Invokebehavior()
    {
        //Debug.Log("Invoked Behavior!");
        //Check if ID is last of the List
        //int tmpLast = CommandPath.FindIndex(CommandPath.Count, x => x.index == currentMove);
        int tmpIndex = CommandPath.Find(x => x.index == currentMove).index;
        int tmpLastIndex = CommandPath.Find(x => x.index == CommandPath.Count - 1).index;
        //Debug.Log("Current Move:" + currentMove + "Path index:" + tmpIndex + "Last index:" +tmpLastIndex);
        //Debug.Log(currentMove == tmpLastIndex);
        if (currentMove == tmpLastIndex)
        {
            currentMove = 0;
            return;
        }
        //Debug.Log(currentMove == tmpIndex);
        //Check is current ID is valid
        if (currentMove == tmpIndex) 
        {
            //Debug.Log(CommandPath.Find(x => x.index == currentMove).Direction);       
            MoveEnemy(CommandPath.Find(x => x.index == currentMove).Direction);          
            //currentDirection = CommandPath.Find(x => x.index == currentMove).Direction;         
        }
        
        currentMove++;
    }
    IEnumerator MoveToTarget()
    {
        while (Vector3.Distance(transform.position, enemyMovePoint.position) > 0f)
        {
            //transform.position = Vector3.Lerp(transform.position, enemyMovePoint.position, speed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, enemyMovePoint.position, speed * Time.deltaTime);
            //Debug.Log("Moving!");
            //Debug.Log("Moving!");
            yield return null;
        }
        //Debug.Log("Moved completed!");
        yield return new WaitWhile(() => Vector3.Distance(transform.position, enemyMovePoint.position) < 0f);
    }
}
