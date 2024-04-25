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

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void RevertToInitialState()
    {
        if (isGateOpen)
            gameObject.SetActive(false);
    }
}
