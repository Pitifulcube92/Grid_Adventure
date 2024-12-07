using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Tile : ISubject
{
    [SerializeField] private float speed;
    [SerializeField] private float movePointDistance;
    [SerializeField] private float movePointRadius;
    [SerializeField] private bool isMoving;
    [SerializeField] Transform movePoint;
    [SerializeField] private Event test;
    [SerializeField] private KeyCode up;
    [SerializeField] private KeyCode down;
    [SerializeField] private KeyCode right;
    [SerializeField] private KeyCode left;
    [SerializeField] private Animator animExplosion;
    [SerializeField] private Sprite evilSkin;
    [SerializeField] private Sprite scarleSkin;
    [SerializeField] private Material defaultMaterial;

    [SerializeField] private LayerMask unWalkable;
    // Start is called before the first frame update
    void Start()
    {
        //cheatcode skins

        if(GameManager.instance.GetCheatInfo().EvilScarlingSkin == true)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = evilSkin;
            gameObject.GetComponent<SpriteRenderer>().material = defaultMaterial;
        }
        if (GameManager.instance.GetCheatInfo().ScarleSkin == true)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = scarleSkin;
            gameObject.GetComponent<SpriteRenderer>().material = defaultMaterial;
        }
        movePoint.parent = null;
    }

    // Update is called once per frame
    public void Update()
    {
        //Debug.Log(transform.position == movePoint.position);
        PlayerMove();
        
    }
    private void FixedUpdate()
    {
        
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(movePoint.position, movePointRadius);
    //}
    void PlayerMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
        if (isMoving)
        {
            if (Vector3.Distance(transform.position, movePoint.position) <= 0.5f && transform.position == movePoint.position)
            {
                if (Input.GetKeyDown(right)|| Input.GetKey(right))
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(movePointDistance, 0f, 0f), movePointRadius, unWalkable))
                    {
                        movePoint.position += new Vector3(movePointDistance, 0f, 0f);
                        GameManager.instance.GetSoundManager().GetSFXSource(2).pitch = UnityEngine.Random.Range(1f, 1.25f);
                        GameManager.instance.GetSoundManager().PlaySFXClip("Retro FootStep 03", true, GameManager.instance.GetSoundManager().GetSFXSource(2));
                        //GameManager.instance.GetSoundManager().GetSFXSource().pitch = 1f;
                    }
                }
                else if (Input.GetKeyDown(left) || Input.GetKey(left))
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(-movePointDistance, 0f, 0f), movePointRadius, unWalkable))
                    {
                        movePoint.position += new Vector3(-movePointDistance, 0f, 0f);
                        GameManager.instance.GetSoundManager().GetSFXSource(2).pitch = UnityEngine.Random.Range(1f, 1.25f);
                        GameManager.instance.GetSoundManager().PlaySFXClip("Retro FootStep 03", true,GameManager.instance.GetSoundManager().GetSFXSource(2));
                        //GameManager.instance.GetSoundManager().GetSFXSource().pitch = 1f;
                    }
                        
                }
                else if (Input.GetKeyDown(up) || Input.GetKey(up))
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, movePointDistance, 0f), movePointRadius, unWalkable))
                    {
                        movePoint.position += new Vector3(0f, movePointDistance, 0f);
                        GameManager.instance.GetSoundManager().GetSFXSource(2).pitch = UnityEngine.Random.Range(1f, 1.25f);
                        GameManager.instance.GetSoundManager().PlaySFXClip("Retro FootStep 03", true, GameManager.instance.GetSoundManager().GetSFXSource(2));
                        //GameManager.instance.GetSoundManager().GetSFXSource().pitch = 1f;
                    }
                      
                }
                else if (Input.GetKeyDown(down) || Input.GetKey(down))
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -movePointDistance, 0f), movePointRadius, unWalkable))
                    {
                        movePoint.position += new Vector3(0f, -movePointDistance, 0f);
                        GameManager.instance.GetSoundManager().GetSFXSource(2).pitch = UnityEngine.Random.Range(1f, 1.25f);
                        GameManager.instance.GetSoundManager().PlaySFXClip("Retro FootStep 03", true, GameManager.instance.GetSoundManager().GetSFXSource(2));
                        //GameManager.instance.GetSoundManager().GetSFXSource().pitch = 1f;
                    }
                       
                }
              
            }
        }
    }
    public Animator GetAnimator()
    {
        return animExplosion;
    }
    public void MovePlayerButton(string dir_)
    {
        switch (dir_)
        {
            case "up":
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, movePointDistance, 0f), movePointRadius, unWalkable))
                    movePoint.position += new Vector3(0f, movePointDistance, 0f);
                break;
            case "down":
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -movePointDistance, 0f), movePointRadius, unWalkable))
                    movePoint.position += new Vector3(0f, -movePointDistance, 0f);
                break;
            case "right":
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(movePointDistance, 0f, 0f), movePointRadius, unWalkable))
                    movePoint.position += new Vector3(movePointDistance, 0f, 0f);
                break;
            case "left":
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(-movePointDistance, 0f, 0f), movePointRadius, unWalkable))
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

    public void SetIsMoving(bool tmp_)
    { isMoving = tmp_; }
    public bool GetIsMoving() 
    { return isMoving; }

}
    