using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Trigger : MonoBehaviour
{
    [SerializeField] private Dialogue_Info dialogue;
    [SerializeField] private SpriteRenderer speakIcon;
    [SerializeField] private bool isTriggered;  

    private void Awake()
    {
        //speakIcon = GameObject.Find("Dialog_icon").GetComponent<SpriteRenderer>();
        isTriggered = false;
    }
    private void Start()
    {
        speakIcon.enabled = false;
    }
    public void TriggerDialogue()
    {
        GameObject.FindObjectOfType<Level_Dialogue_Component>().StartDialogue(dialogue,this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            speakIcon.enabled = true;
            TriggerDialogue();
        }
          
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            speakIcon.enabled = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //if(collision.tag == "Player") //&& Input.GetMouseButton(0))
        //{    
        //    if(isTriggered == false)
        //    {
        //        if (Input.GetKeyUp(KeyCode.Space))
        //        {
        //            Debug.Log("Pressed!");
        //            TriggerDialogue();
        //            //isTriggered = true;
        //        }
        //    }
        //}
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
