using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Tile : BaseInteractionTile
{
    //[SerializeField] private Gate_Tile gate_1;
    //[SerializeField] private Gate_Tile gate_2;
    [SerializeField] private List<Gate_Tile> gates;
    [SerializeField] private bool isFliped;

    // Start is called before the first frame update
    void Start()
    {
        basePosition = gameObject.transform;
        baseSpriteRender = gameObject.GetComponent<SpriteRenderer>();
        if (isFliped)
        {
            baseSpriteRender.transform.Rotate(0.0f, 180.0f, 0.0f);
        }
        if (!baseSpriteRender)
        {
            Debug.LogError("No gates or sprite is not assigned!!");
        }
        if(gates.Count == 0)
        {
            Debug.LogError("No gates are assigned");
        }

      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Note:switch the sprite and have the gate disable behaviour in the gate object
        Debug.Log(collision.name);
        if (collision.tag == "Player")
        {
            GameManager.instance.GetSoundManager().PlaySFXClip("lever_metal");
            if(isFliped)
                baseSpriteRender.transform.Rotate(0.0f, -180.0f, 0.0f);
            else
                baseSpriteRender.transform.Rotate(0.0f, 180.0f, 0.0f);

            foreach(Gate_Tile x in gates)   
            {
                x.ActivateGate();
            }
        }
    }
    override public void RevertToInitialState()
    {
        //Return state of gate
        Debug.Log("Switch call!");
        float tmpY_ = baseSpriteRender.transform.rotation.y;
        if(tmpY_ == 1)
        {
            baseSpriteRender.transform.Rotate(0.0f, -180.0f, 0.0f);
        }
    }
}
