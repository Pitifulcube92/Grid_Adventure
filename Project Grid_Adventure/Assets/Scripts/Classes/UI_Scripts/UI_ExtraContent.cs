using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_ExtraContent : BaseUIScript
{
    [SerializeField] private RawImage backgroundImg;
    [SerializeField] private Button backBtn;
    [SerializeField] private float y, x;

    public override void SetUIConfigure()
    {
        GameObject.Find("Back btn").GetComponent<Button>();
        backBtn.onClick.AddListener(delegate { GameManager.instance.GetSoundManager().PlaySFXClip("Retro_Blop_18"); });
        gameObject.GetComponent<Canvas>().worldCamera = GameObject.FindObjectOfType<Camera>();
    }
        // Start is called before the first frame update
        void Start()
    {
        gameObject.GetComponent<Canvas>().worldCamera = GameObject.FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        backgroundImg.uvRect = new Rect(backgroundImg.uvRect.position + new Vector2(x, y) * Time.deltaTime, backgroundImg.uvRect.size);
    }
}
