using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Fragment_Tile : BaseInteractionTile
{

    // Start is called before the first frame update
    void Start()
    {
        if (base.basePosition == null)
            base.basePosition = transform;
        if (base.baseSpriteRender == null)
            base.baseSpriteRender = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player_Tile>().NotifyObserver(PlayerState.Interact_Fragment_Key);
            gameObject.SetActive(false);
            Debug.Log("Grabbed Fragement Key");
        }
    }

    public override void RevertToInitialState()
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
    }

}
