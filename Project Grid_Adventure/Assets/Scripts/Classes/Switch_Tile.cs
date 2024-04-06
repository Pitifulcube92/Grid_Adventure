using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Tile : BaseInteractionTile
{
    [SerializeField] private Gate_Tile gate;
    [SerializeField] private bool initActiveSelf;
    // Start is called before the first frame update
    void Start()
    {
        basePosition = gameObject.transform;
        baseSpriteRender = gameObject.GetComponent<SpriteRenderer>();
        initActiveSelf = gate.gameObject.activeSelf;
        if (!gate || !baseSpriteRender)
        {
            Debug.LogError("No gate or sprite is not assigned!!");
        }

      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.tag == "Player")
        {
            GameManager.instance.GetSoundManager().PlaySFXClip("lever_metal");
            if (gate.gameObject.activeSelf == true)
            {
                //switch sprite!
                baseSpriteRender.transform.Rotate(0.0f, 180.0f, 0.0f);
                gate.gameObject.SetActive(false);
                Debug.Log("Rotation y: " + baseSpriteRender.transform.rotation.y);
            }
            else
            {
                baseSpriteRender.transform.Rotate(0.0f, -180.0f, 0.0f);
                gate.gameObject.SetActive(true);
                Debug.Log("Rotation y: " + baseSpriteRender.transform.rotation.y);
            }
           
        }
    }
    public GameObject GetGate()
    {
        return gate.gameObject;
    }

    override public void RevertToInitialState()
    {
        //Return state of gate
        Debug.Log("Switch call!");
        gate.gameObject.SetActive(initActiveSelf);
        float tmpY_ = baseSpriteRender.transform.rotation.y;
        if(tmpY_ == 1)
        {
            baseSpriteRender.transform.Rotate(0.0f, -180.0f, 0.0f);
        }
    }
}
