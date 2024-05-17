using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_Tile : BaseInteractionTile
{
    [Header("Info")]
    [SerializeField] private bool isGateOpen;
 
    // Start is called before the first frame update
    void Start()
    {
        if (isGateOpen)
            gameObject.SetActive(false);

        if (base.basePosition == null)
            base.basePosition = transform;
        if (base.baseSpriteRender == null)
            base.baseSpriteRender = gameObject.GetComponent<SpriteRenderer>();
    }
    public void SetIsGateOpen(bool tmp_)
    {
        isGateOpen = tmp_;
    }
    public void ActivateGate()
    {
        if (isGateOpen)
        {
            gameObject.SetActive(true);
            isGateOpen = false;
        }
        else
        {
            gameObject.SetActive(false);
            isGateOpen = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            collision.GetComponent<Player_Tile>().NotifyObserver(PlayerState.Taken_Damage);
        }
    }

    public override void RevertToInitialState()
    {
        if (isGateOpen)
            gameObject.SetActive(false);
    }
}
