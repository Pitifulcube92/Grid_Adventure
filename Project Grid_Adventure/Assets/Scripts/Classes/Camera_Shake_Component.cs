using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Shake_Component : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;
    // Start is called before the first frame update
    public void CallCameraShake(float duration_, float magnitude_)
    {
        StartCoroutine(ShakeCamera(duration_,magnitude_));
    }
    IEnumerator ShakeCamera(float duration_, float magnitude_)
    {
        Vector3 originalPos = transform.position;
        float elapsed = 0.0f;

        while(elapsed < duration_)
        {
            float x = Random.Range(-1f, 1f) * magnitude_;
            float y = Random.Range(-1f, 1f) * magnitude_;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
