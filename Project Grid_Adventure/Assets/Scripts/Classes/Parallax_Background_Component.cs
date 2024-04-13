using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax_Background_Component : Base_Level_Component
{
    [Header("Info")]
    [SerializeField] private List<Transform> backgrounds;
    [SerializeField] private List<float> parallaxScales;
    [SerializeField, Range(0, 1)] private float smoothing;

    [SerializeField] private Transform cam;
    [SerializeField] private Vector3 prevCampos;
    private void Awake()
    {
        cam = Camera.main.transform;
    }
    public override void InitalizeComponent()
    {
        prevCampos = cam.position;
        parallaxScales = new List<float>();

        foreach(Transform x in backgrounds)
        {
            parallaxScales.Add(x.position.z*-1);
        }
    }

    public override void ResetComponent()
    {
        return;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform x in backgrounds)
        {
            float parallax_X = (prevCampos.x - cam.position.x) * parallaxScales.Find(y => y == x.position.z*-1);
            float parallax_Y = (prevCampos.y - cam.position.y) * parallaxScales.Find(y => y == x.position.z * -1);
            float backgroundTargetPosX = x.position.x + parallax_X;
            float backgroundTargetPosY = x.position.y + parallax_Y;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundTargetPosY, x.position.z);

            x.position = Vector3.Lerp(x.position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        prevCampos = cam.position;
    }
}
