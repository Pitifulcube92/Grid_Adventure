using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Tile : ISubject
{
    [SerializeField] private float speed;
    [SerializeField] private float movePointDistance;
    [SerializeField] Transform movePoint;
    [SerializeField] private KeyCode up;
    [SerializeField] private KeyCode down;
    [SerializeField] private KeyCode right;
    [SerializeField] private KeyCode left;

    [SerializeField] private LayerMask unWalkable;
    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    public void Update()
    {
        //Debug.Log(transform.position == movePoint.position);
        PlayerMove();
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(movePoint.position, 0.1f);
    }
    void PlayerMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed*Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.5f && transform.position == movePoint.position)
        {
            if (Input.GetKeyDown(right))
            {
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(movePointDistance, 0f, 0f), 0.2f, unWalkable))
                    movePoint.position += new Vector3(movePointDistance, 0f, 0f);
            }
            else if (Input.GetKeyDown(left))
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(-movePointDistance, 0f, 0f), 0.2f, unWalkable))
                    movePoint.position += new Vector3(-movePointDistance, 0f, 0f);
            }
            else if (Input.GetKeyDown(up))
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, movePointDistance, 0f), 0.2f, unWalkable))
                    movePoint.position += new Vector3(0f, movePointDistance, 0f);
            }
            else if (Input.GetKeyDown(down))
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -movePointDistance, 0f), 0.2f, unWalkable))
                    movePoint.position += new Vector3(0f, -movePointDistance, 0f);
            }
        }
    }
    public void MovePlayerButton(string dir_)
    {
        switch (dir_)
        {
            case "up":
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, movePointDistance, 0f), 0.2f, unWalkable))
                    movePoint.position += new Vector3(0f, movePointDistance, 0f);
                break;
            case "down":
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -movePointDistance, 0f), 0.2f, unWalkable))
                    movePoint.position += new Vector3(0f, -movePointDistance, 0f);
                break;
            case "right":
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(movePointDistance, 0f, 0f), 0.2f, unWalkable))
                    movePoint.position += new Vector3(movePointDistance, 0f, 0f);
                break;
            case "left":
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(-movePointDistance, 0f, 0f), 0.2f, unWalkable))
                    movePoint.position += new Vector3(-movePointDistance, 0f, 0f);
                break;
        }
    }

    public void ChangePosition(Vector3 tmp_)
    {
        //Debug.Log(tmp_);
        gameObject.transform.SetLocalPositionAndRotation(tmp_, gameObject.transform.rotation);
        movePoint.transform.SetLocalPositionAndRotation(tmp_, gameObject.transform.rotation);
    }
}
    