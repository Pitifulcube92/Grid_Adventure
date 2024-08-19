using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Canonball : MonoBehaviour
{
    [Header("Info")]
    //[SerializeField,Range(0,10)]
    private float speed;
    [SerializeField] private Rigidbody2D rig2D;
    //[SerializeField] private Transform lunchPoint;
    // Start is called before the first frame update
    void Start()
    {
        rig2D = gameObject.GetComponent<Rigidbody2D>();
        rig2D.freezeRotation = true;
    }
    public void SetSpeed(float speed_)
    {
        speed = speed_;
    }
    // Update is called once per frame
    void Update()
    {
        //rig2D.AddForce(transform.right * speed, ForceMode2D.Impulse);
        rig2D.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player_Tile>().NotifyObserver(PlayerState.Taken_Damage);
            Destroy(gameObject);
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("unWalkable"))
        {
            Debug.Log(collision.name);
            Destroy(gameObject);
        }
        
    }
}
