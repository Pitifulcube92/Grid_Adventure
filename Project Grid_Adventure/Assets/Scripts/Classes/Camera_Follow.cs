using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private Transform player;
    [SerializeField] bool canMove;
    [SerializeField,Range(0,1)] private float smoothingSpeed;
    // Start is called before the first frame update
    void Start()
    {
        if (!centerPoint)
            centerPoint = gameObject.transform;
        if (!player)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        if (canMove)
        {
            gameObject.transform.position = new Vector3(GameObject.Find("Start Position").transform.position.x,
                                                        GameObject.Find("Start Position").transform.position.y,
                                                        gameObject.transform.position.z);
        }
                
    }


    private bool CheckDistance()
    {
        //Debug.Log(Vector2.Distance(centerPoint.position, player.position));
        if (Vector2.Distance(centerPoint.position,player.position) > maxDistance)
        {
            return true;
        }
        return false;
    }

    public IEnumerator MoveCamera()
    {
        Debug.Log(CheckDistance());
        while (Vector2.Distance(transform.position, player.position) > 0f)
        {
            Debug.Log("moving");
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(player.position.x, player.position.y, gameObject.transform.position.z), smoothingSpeed * Time.deltaTime);
            //Vector3.Lerp(gameObject.transform.position, player.position, smoothingSpeed);
            yield return null;      
        }

        Debug.Log("Moved completed!");
        yield return new WaitWhile(() => CheckDistance() == true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(centerPoint.position, maxDistance);
    }
    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            ////Debug.Log(CheckDistance());
            if (CheckDistance())
            {
                StartCoroutine(MoveCamera());
                StopCoroutine(MoveCamera());
            }
        }
    }
}
