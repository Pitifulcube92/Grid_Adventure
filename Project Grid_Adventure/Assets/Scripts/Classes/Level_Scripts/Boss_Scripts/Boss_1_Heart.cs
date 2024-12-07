using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Heart : BaseInteractionTile
{
    [SerializeField] private Boss_1_Level_Component bossRef;

    public override void RevertToInitialState()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            bossRef.DamageBoss();
            GameManager.instance.GetSoundManager().PlaySFXClip("retro explode", false, GameManager.instance.GetSoundManager().GetSFXSource(1));
            gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        bossRef = GameObject.FindObjectOfType<Boss_1_Level_Component>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
