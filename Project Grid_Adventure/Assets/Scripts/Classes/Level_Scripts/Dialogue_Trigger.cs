using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Trigger : MonoBehaviour
{
    [SerializeField] private Dialogue_Info dialogue;
    [SerializeField] private SpriteRenderer speakIcon;
    [SerializeField] private bool isTriggered;
    [SerializeField] private Level_Dialogue_Component dialogue_Comp;

    private void Awake()
    {
        //speakIcon = GameObject.Find("Dialog_icon").GetComponent<SpriteRenderer>();
        isTriggered = false;
        if (dialogue_Comp == null)
        {
            dialogue_Comp = GameObject.FindObjectOfType<Level_Dialogue_Component>();
        }
    }
    private void Start()
    {
        if(speakIcon != null)
                speakIcon.enabled = false;
    }
    public void TriggerDialogue()
    {
        dialogue_Comp.StartDialogue(dialogue,this);
        dialogue_Comp.DisplayNextSentence();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            speakIcon.enabled = true;
            dialogue_Comp.SetcanTriggerDialog(true);
            TriggerDialogue();
        }
          
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            speakIcon.enabled = false;
            dialogue_Comp.SetcanTriggerDialog(false);
        }
        //GameObject.FindObjectOfType<Level_Dialogue_Component>().StartDialogue(new Dialogue_Info(), this);
    }

    public void ResetTrigger()
    {
        TriggerDialogue();
         
        //isTriggered = false;
    }
    public bool GetTriggerFlag()
    {
        return isTriggered;
    }
}
