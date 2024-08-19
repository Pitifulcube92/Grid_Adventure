using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Level_Dialogue_Component : Base_Level_Component
{
    [SerializeField] private Queue<string> sentences;
    [SerializeField] private Dialogue_Trigger trigger;
    [SerializeField] private Text dialogueName;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Image dialogImage;
    [SerializeField] private KeyCode continueBtn;
    [SerializeField] private bool initalConvo;
    [SerializeField] private Animator anim;

    /// <summary>
    /// Note: Add a disable player controls to not move while conversing
    /// </summary>
    private void Update()
    {
 
            if (Input.GetKeyDown(continueBtn)) //&& pressedOnce == true)
            {
                if (initalConvo == true)
                {
                    anim.SetBool("isDialogueBoxOpen",true);
                }
                Debug.Log("Input Called");
                //pressedOnce = false;
                initalConvo = false;
                DisplayNextSentence();
            }
    }
    private void Awake()
    {
        dialogueText = GameObject.Find("Dialogue_Context").GetComponent<Text>();
        dialogueName = GameObject.Find("Dialogue_Name").GetComponent<Text>();
        dialogImage = GameObject.Find("Dialogue_Image").GetComponent<Image>();
        anim = GameObject.Find("Dialogue_Box").GetComponent<Animator>();
        trigger = gameObject.AddComponent<Dialogue_Trigger>();
        continueBtn = KeyCode.Space;
    }
    public override void InitalizeComponent()
    {
        sentences = new Queue<string>();
        //if (trigger == null)
        //    Debug.LogWarning("trigger not assigned!");
      
        initalConvo = false;
    }
    public void StartDialogue(Dialogue_Info dialogue_, Dialogue_Trigger trigger_)
    {
        Debug.Log("A conversation has started with " + dialogue_.name);

        sentences.Clear();
        trigger = null;
        dialogueName.text = "";
        if(dialogue_.hasImage == false)
        {
            dialogImage.enabled = false;
        }
        if(dialogue_.hasImage == true)
        {

            dialogImage.sprite = dialogue_.targetImage;
            dialogImage.enabled = true;
        }

        foreach (string sentence in dialogue_.sentences)
        {
            sentences.Enqueue(sentence);
        }
        trigger = trigger_;
        dialogueName.text = dialogue_.name;
        initalConvo = true;
        //DisplayNextSentence();
        
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
    
        string sentence_ = sentences.Dequeue();
        Debug.Log(sentence_);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence_));

        //dialogueText.text = sentence_;

    }
    IEnumerator TypeSentence(string sentence_)
    {
        dialogueText.text = "";
        foreach(char letter in sentence_.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.07f);
        }
    } 

    public void EndDialogue()
    {
        Debug.Log("Dialogue has ended!");
        anim.SetBool("isDialogueBoxOpen", false);
        trigger.ResetTrigger();
    }  

    public override void ResetComponent()
    {
        return;
    }
}
