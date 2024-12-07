using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private bool fadeIn;
    [SerializeField] private bool fadeOut;
    [SerializeField] private Canvas targetCanvas;
    [SerializeField] private Image targetImage;
    [SerializeField] private float imgAlpha;
    private void Awake()
    {
        targetImage = GameObject.Find("FadeImage").GetComponent<Image>();
        targetCanvas = GameObject.Find("FadeCanvas").GetComponent<Canvas>();
        if (!targetCanvas)
        {
            Debug.Log("target Canvas not found!");
        }
        //targetCanvas.color = new Color(0, 0, 0, 0);
        imgAlpha = targetImage.color.a;
    }
    void Start()
    {
        gameObject.GetComponent<Canvas>().worldCamera = GameObject.FindAnyObjectByType<Camera>();
        gameObject.GetComponent<Canvas>().sortingLayerName = "UI";
        //gameObject.GetComponent<Canvas>().sortingLayerID = 2;
    }
    public IEnumerator FadeIn()
    {
        Color c = targetImage.color;
        for(float alpha = c.a; alpha >= 0; alpha -= 0.1f)
        {
            c.a = alpha;
            targetImage.color = c;
            //Debug.Log("Fading In...");
            yield return new WaitForSeconds(.1f);
        }
        targetCanvas.sortingOrder = 0;
        yield return new WaitForSeconds(.11f);
       // Debug.Log("Done Fading In!");
    }

    public IEnumerator FadeOut()
    {
        Color c = targetImage.color;
        targetCanvas.sortingOrder = 2;
        for (float alpha = c.a; alpha <= 2f; alpha += 0.1f)
        {
            c.a = alpha;
            targetImage.color = c;
            //Debug.Log("Fading Out...");
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(.11f);
        //Debug.Log("Done Fading Out!");
    }
    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown("f"))
    //    {
    //        StartCoroutine(FadeIn());
    //    }

    //    if (Input.GetKeyDown("g"))
    //    {
    //        StartCoroutine(FadeOut());
    //    }
    //}
}
