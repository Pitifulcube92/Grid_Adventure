using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_3_Switch_Script : BaseInteractionTile
{
    [SerializeField] private bool isFliped;
    [SerializeField] private float challengeTime;
    [SerializeField] private Boss_3_Level_Component bossRef;

    public override void RevertToInitialState()
    {
        float tmpY_ = baseSpriteRender.transform.rotation.y;
        if (tmpY_ == 1)
        {
            baseSpriteRender.transform.Rotate(0.0f, -180.0f, 0.0f);
        }
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log(collision.name);
        if (collision.tag == "Player")
        {
            GameManager.instance.GetSoundManager().PlaySFXClip("lever_metal");
            if (isFliped)
                baseSpriteRender.transform.Rotate(0.0f, -180.0f, 0.0f);
            else
                baseSpriteRender.transform.Rotate(0.0f, 180.0f, 0.0f);
        }

        bossRef.StartChallege(challengeTime, gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        basePosition = gameObject.transform;
        baseSpriteRender = gameObject.GetComponent<SpriteRenderer>();
        bossRef = GameObject.FindObjectOfType<Boss_3_Level_Component>();
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
}
