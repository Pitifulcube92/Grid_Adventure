using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public abstract class BaseEnemy_Tile : MonoBehaviour
{

    [SerializeField] protected Transform enemyMovePoint;
    [SerializeField] protected LayerMask unWalkable;
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


    public abstract void Invokebehavior();
}
