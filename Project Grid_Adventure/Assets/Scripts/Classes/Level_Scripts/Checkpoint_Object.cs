using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Object : BaseInteractionTile
{
    // Update is called once per frame
    [Header("Info")]
    [SerializeField] private Collider2D collidor;
    [SerializeField] private SpriteRenderer render;
    [SerializeField] private Sprite checkPointActive;
    private void Start()
    {
        collidor = gameObject.GetComponent<BoxCollider2D>();
        render = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player_Tile>().NotifyObserver(PlayerState.Interact_Checkpoint);
            collision.GetComponent<Player_Tile>().ChangePosition(gameObject.transform.position);
            render.sprite = checkPointActive;
            collidor.enabled = false;
        }
    }
    public override void RevertToInitialState()
    {
        return;
    }
}
