using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Switch_Script : BaseInteractionTile
{
    [SerializeField] private bool isFliped;
    [SerializeField] private bool isActivated;
    [SerializeField] private char keySwitch;
    [SerializeField] private Boss_2_Level_Component bossRef;
    public override void RevertToInitialState()
    {
        float tmpY_ = baseSpriteRender.transform.rotation.y;
        if (tmpY_ == 1)
        {
            baseSpriteRender.transform.Rotate(0.0f, -180.0f, 0.0f);
        }
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        basePosition = gameObject.transform;
        baseSpriteRender = gameObject.GetComponent<SpriteRenderer>();
        bossRef = GameObject.FindObjectOfType<Boss_2_Level_Component>();
        isActivated = false;
        if (isFliped)
        {
            baseSpriteRender.transform.Rotate(0.0f, 180.0f, 0.0f);
        }
        if (!baseSpriteRender)
        {
            Debug.LogError("No gates or sprite is not assigned!!");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetIsFlipped(bool tmp_)
    {
        isFliped = tmp_;
    }
    public bool GetIsFlipped()
    {
        return isFliped;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.instance.GetSoundManager().PlaySFXClip("lever_metal",false, GameManager.instance.GetSoundManager().GetSFXSource(1));
            if (isFliped)
                baseSpriteRender.transform.Rotate(0.0f, -180.0f, 0.0f);
            else
                baseSpriteRender.transform.Rotate(0.0f, 180.0f, 0.0f);
        }
        isFliped = true;
        bossRef.SendKey(keySwitch);
        bossRef.EvaluateKey();
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        
    }
}
