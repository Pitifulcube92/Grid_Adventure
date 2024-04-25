using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTile : BaseInteractionTile
{
    [Header("Info")]
    [SerializeField] private bool initiallyActive;
    // Start is called before the first frame update
    void Start()
    {
        if(base.basePosition == null)
            base.basePosition = transform;
        if (base.baseSpriteRender == null)
            base.baseSpriteRender = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player_Tile>().NotifyObserver(PlayerState.Interact_Key);
            Debug.Log("Grabbed Key");
        }
    }
    public void SetInitialActivity(bool tmp_) {
        initiallyActive = tmp_;
        gameObject.SetActive(tmp_);
    }
    override public void RevertToInitialState()
    {
        gameObject.SetActive(initiallyActive);
    }
}
