using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraViewPortManager : MonoBehaviour
{
    [SerializeField] private Camera primeCam;
    [SerializeField] private float sceneWidth = 10;
    [SerializeField] private float sceneheight = 10;
    [SerializeField] private Vector3 offSet;
    // Start is called before the first frame update
    void Start()
    {
        if (!primeCam)
        {
            Debug.Log("Camera is null!");
        }
    }

    public void AdjustCameraSize()
    {
        float unitPerPixel = sceneWidth / Screen.width;
        float desiredHeightSize = 0.5f * unitPerPixel * Screen.height;
        primeCam.orthographicSize = desiredHeightSize;
    }
    public void OffsetCamera()
    {
        float unitPerPixelWidth = sceneWidth / Screen.width;
        float unitPerPixelHeight = sceneheight / Screen.height;
        Vector3 result = new Vector3(offSet.x * unitPerPixelWidth , offSet.y * unitPerPixelHeight, offSet.z);
        primeCam.transform.position = offSet;
    }

    // Update is called once per frame
    void Update()
    {
        AdjustCameraSize();
        OffsetCamera();
    }
}
