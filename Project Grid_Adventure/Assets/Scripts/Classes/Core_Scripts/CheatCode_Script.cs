using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatCode_Script : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private InputField cheatCodeInput;
    [SerializeField] private Text activateText;
    private void Start()
    {
        activateText.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    public void CheckText()
    {
        switch (cheatCodeInput.text)
        {
            case "scarlingforever":
                //Turn on Cheat code bool
                GameManager.instance.ActivateCheat(cheatCodeInput.text);
                StopAllCoroutines();
                ChangeTextNotification("Cheat has been activated!!!");
                StartCoroutine("displayActivateText");
                break;

            case "evilscarling":
                //Turn on Cheat code bool
                GameManager.instance.ActivateCheat(cheatCodeInput.text);
                StopAllCoroutines();
                ChangeTextNotification("Cheat has been activated!!!");
                StartCoroutine("displayActivateText");
                break;
            case "scarlingscarle":
                //Turn on Cheat code bool
                GameManager.instance.ActivateCheat(cheatCodeInput.text);
                StopAllCoroutines();
                ChangeTextNotification("Cheat has been activated!!!");
                StartCoroutine("displayActivateText");
                break;
            case "freescarling":
                //Turn on Cheat code bool
                GameManager.instance.ActivateCheat(cheatCodeInput.text);
                StopAllCoroutines();
                ChangeTextNotification("Cheat has been activated!!!");
                StartCoroutine("displayActivateText");
                break;
        }
    }
    public void ResetCheats()
    {
        GameManager.instance.ResetCheats();
        StopAllCoroutines();
        ChangeTextNotification("Cheat has been Reset!!!");
        StartCoroutine("displayActivateText");
    }
    void ChangeTextNotification(string tmp_)
    {
        activateText.text = tmp_;
    }
    IEnumerator displayActivateText()
    {
        activateText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        activateText.gameObject.SetActive(false);
        GameManager.instance.GetUIManager().ChangeUI("ExtraContentUI");
    } 
}
