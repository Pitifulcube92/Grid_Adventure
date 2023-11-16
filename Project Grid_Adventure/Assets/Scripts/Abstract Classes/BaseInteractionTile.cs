using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractionTile : MonoBehaviour
{
    [SerializeField] protected Transform basePosition;
    [SerializeField] protected SpriteRenderer baseSpriteRender;
    public abstract void RevertToInitialState();
    public Transform GetBasePosition() { return basePosition; }
    public SpriteRenderer GetSpriteRenderer() {return baseSpriteRender;}
}
