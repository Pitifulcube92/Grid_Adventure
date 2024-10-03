using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleprt_Tile_set : BaseInteractionTile
{
    [Header("Info")]
    [SerializeField] private Transform teleportOut;
    // Start is called before the first frame update
    void Start()
    {
        teleportOut = gameObject.transform.GetChild(0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(Teleport(collision));
           
        }
    }
    public IEnumerator Teleport(Collider2D tmp_)
    {
        yield return new WaitForSeconds(0.25f);
        tmp_.GetComponent<Player_Tile>().ChangePosition(teleportOut.position);
        GameManager.instance.GetSoundManager().PlaySFXClip("Retro Magic Protection 01");
    }
    // Update is called once per frame
    public override void RevertToInitialState()
    {
        return;
    }

}
