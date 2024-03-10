using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private bool fadeIn;
    [SerializeField] private bool fadeOut;
    [SerializeField] private Image targetCanvas;
    [SerializeField] private float imgAlpha;
    private void Awake()
    {
        targetCanvas = gameObject.GetComponentInChildren<Image>();
        if (!targetCanvas)
        {
            Debug.Log("target Canvas not found!");
        }
        //targetCanvas.color = new Color(0, 0, 0, 0);
        imgAlpha = targetCanvas.color.a;
    }
    void Start()
    {
      
    }
    public IEnumerator FadeIn()
    {
        Color c = targetCanvas.color;
        for(float alpha = c.a; alpha >= 0; alpha -= 0.1f)
        {
            c.a = alpha;
            targetCanvas.color = c;
            Debug.Log("Fading In...");
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(.11f);
        Debug.Log("Done Fading In!");
    }

    public IEnumerator FadeOut()
    {
        Color c = targetCanvas.color;
        for(float alpha = c.a; alpha <= 2f; alpha += 0.1f)
        {
            c.a = alpha;
            targetCanvas.color = c;
            Debug.Log("Fading Out...");
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(.1f);
        Debug.Log("Done Fading Out!");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            StartCoroutine(FadeIn());
        }

        if (Input.GetKeyDown("g"))
        {
            StartCoroutine(FadeOut());
        }
    }
}
