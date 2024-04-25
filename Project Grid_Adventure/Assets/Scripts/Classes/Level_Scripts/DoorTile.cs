using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTile : BaseInteractionTile
{
    [SerializeField] private string NextStageName;
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
            gameObject.GetComponent<DoorTile>().baseSpriteRender.color = Color.black;
        }
    }

    override public void RevertToInitialState()
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
    }

    public string ReturnNextStageName()
    {
        return NextStageName;
    }
}
