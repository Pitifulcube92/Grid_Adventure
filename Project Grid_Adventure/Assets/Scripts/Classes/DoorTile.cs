using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTile : BaseInteractionTile
{
    // Start is called before the first frame update
    void Start()
    {
        if (base.basePosition == null)
            base.basePosition = transform;
        if (base.baseSpriteRender == null)
            base.baseSpriteRender = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player_Tile>().NotifyObserver(PlayerState.Interact_Door);
        }
    }

    override public void RevertToInitialState()
    {
        throw new System.NotImplementedException();
    }
}
