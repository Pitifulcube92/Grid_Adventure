using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Checkpoint_Object : BaseInteractionTile
{
    // Update is called once per frame
    [Header("Info")]
    [SerializeField] private GameObject checkpointLight;
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
            //collision.GetComponent<Player_Tile>().NotifyObserver(PlayerState.Interact_Checkpoint);

            //collision.GetComponent<Player_Tile>().ChangePosition(gameObject.transform.position);
            GameManager.instance.GetSoundManager().PlaySFXClip("Retro Charge Magic 11",false, GameManager.instance.GetSoundManager().GetSFXSource(1));
            FindObjectOfType<Level_Observer>().ChangePlayerSpawnPos(gameObject.transform);
            collision.GetComponent<Player_Tile>().NotifyObserver(PlayerState.Interact_Checkpoint);
            checkpointLight.GetComponent<Light2D>().color = new Color(39f, 98f, 5f, 0.1f);
            render.sprite = checkPointActive;
            collidor.enabled = false;
            //Destroy(gameObject);

        }
    }
    public override void RevertToInitialState()
    {
        return;
    }
}
