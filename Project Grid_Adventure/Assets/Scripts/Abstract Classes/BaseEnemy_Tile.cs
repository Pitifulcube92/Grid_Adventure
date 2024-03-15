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

public abstract class BaseEnemy_Tile : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float movePointDistance;
    [SerializeField] protected Transform enemyMovePoint;
    [SerializeField] protected List<EnemyMoveCommand> CommandPath;
    [SerializeField] protected LayerMask unWalkable;
    [SerializeField] protected EnemyDirection currentDirection;
    [SerializeField] protected float maxTimer;
    [SerializeField] protected float tmpTimer;
    // Start is called before the first frame update
    void Awake()
    {
        tmpTimer = maxTimer;
    }
    void Start()
    {
        enemyMovePoint.parent = null;
        //currentDirection = EnemyDirection.ED_NONE;
        StopCoroutine("MoveToTarget");
        StartCoroutine(MoveToTarget());
    }
    public void baseStart()
    {
        Start();
    }
    // Update is called once per frame
    void Update()
    {
        //Behavior is controlled through Timer
        EnemyTimer();
    }
    public void BaseUpdate()
    {
        Update();
    }
    private void FixedUpdate()
    {
        EnemyTimer();
        //MoveEnemy(currentDirection);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(enemyMovePoint.position, 0.1f);
    }
    public void MoveEnemy(EnemyDirection tmp_)
    {
        if(Vector3.Distance(transform.position, enemyMovePoint.position) <= 0.5f && transform.position == enemyMovePoint.position)
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
            else if(tmp_ == EnemyDirection.ED_DOWN)
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player_Tile>().NotifyObserver(PlayerState.Taken_Damage);
        }
    }
    public void ChangePosition(Vector3 tmp_)
    {
        //Debug.Log(tmp_);
        gameObject.transform.SetLocalPositionAndRotation(tmp_, gameObject.transform.rotation);
        enemyMovePoint.transform.SetLocalPositionAndRotation(tmp_, gameObject.transform.rotation);
    }

    void EnemyTimer()
    {
        if(tmpTimer >= 0.0f)
        {
            tmpTimer -= Time.deltaTime;
        }
        else
        {         

            //StopCoroutine("MoveToTarget");
            //StartCoroutine(MoveToTarget());
            Invokebehavior();
            tmpTimer = maxTimer;
        }
    }
    IEnumerator MoveToTarget()
    {
        while (Vector3.Distance(transform.position, enemyMovePoint.position) > 0f)
        {
            //transform.position = Vector3.Lerp(transform.position, enemyMovePoint.position, speed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, enemyMovePoint.position, speed * Time.deltaTime);
            Debug.Log("Moving!");
            yield return null;
        }
        //Debug.Log("Moved completed!");
        yield return new WaitWhile(() => Vector3.Distance(transform.position, enemyMovePoint.position) < 0f);
    }

    public abstract void Invokebehavior();
}
