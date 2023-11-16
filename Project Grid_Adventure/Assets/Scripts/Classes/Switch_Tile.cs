using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Tile : BaseInteractionTile
{
    [SerializeField] private GameObject gate;
    [SerializeField] private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        if (!gate || !sprite)
        {
            Debug.LogError("No gate or sprite is not assigned!!");
        }

      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.tag == "Player")
        {
            if (gate.activeSelf == true)
            {
                //switch sprite!
                sprite.transform.Rotate(0.0f, 180.0f, 0.0f);
                gate.SetActive(false);
            }
            else
            {
                sprite.transform.Rotate(0.0f, 180.0f, 0.0f);
                gate.SetActive(true);
            }
           
        }
    }

    override public void RevertToInitialState()
    {
        throw new System.NotImplementedException();
    }
}
