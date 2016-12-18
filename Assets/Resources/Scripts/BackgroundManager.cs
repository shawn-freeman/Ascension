using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class BackgroundManager : MonoBehaviour
{

    public static float STAGE_WIDTH_RATIO;
    public static float STAGE_HEIGHT_RATIO;

    public GameObject mainCamera;

    public string BackgroundController;
    public string ParallaxKey = "ParallaxBkgd";
    public string StaticKey = "";
    public float ParallaxBaseSpeed;

    public List<GameObject> StaticSpeedBkgds;

    private List<List<GameObject>> parallaxBkgds;

    void Awake()
    {
        Debug.Log("Main Awake Executed!");
        Debug.Log(Screen.width + " " + Screen.height);
        STAGE_WIDTH_RATIO = Screen.width / 800.0f;
        STAGE_HEIGHT_RATIO = Screen.height / 600.0f;
        Debug.Log("STAGE_WIDTH_RATIO: " + STAGE_WIDTH_RATIO + " " +
                    "STAGE_HEIGHT_RATIO: " + STAGE_HEIGHT_RATIO);

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        StaticSpeedBkgds = GameObject.FindGameObjectsWithTag("StaticSpeedBkgd").ToList();

        var i = 1;
        var layer = GameObject.FindGameObjectsWithTag(ParallaxKey + i);
        parallaxBkgds = new List<List<GameObject>>();
        parallaxBkgds.Add(layer.ToList());

        while (parallaxBkgds.Count() > 0)
        {
            try
            {
                i++;
                layer = GameObject.FindGameObjectsWithTag(ParallaxKey + i);
                parallaxBkgds.Add(layer.ToList());
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
                break;
            }
        }

    }

    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        Vector3 tempVector = Vector3.zero;
        for (int layer = 0; layer < parallaxBkgds.Count; layer++)
        {
            //create parallax speed based on 'layer'
            tempVector.y = (ParallaxBaseSpeed / (layer + 1)) * Time.deltaTime;
            //move the backgrounds
            parallaxBkgds[layer] = parallaxBkgds[layer].Select(a => {
                a.transform.position += tempVector;
                AdjustBackground(a);
                return a;
            }).ToList();

        }
    }

    private GameObject AdjustBackground(GameObject bkgd)
    {
        float camDistToThis = Mathf.Abs(Camera.main.transform.position.z - bkgd.transform.position.z);
        Vector3 screenToPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, camDistToThis));
        
        SpriteRenderer renderer = bkgd.GetComponent<SpriteRenderer>();

        //check if top of sprite has reached off the screen
        if (bkgd.transform.position.y + renderer.bounds.extents.y <= screenToPoint.y)
        {
            bkgd.transform.position = new Vector3(0, renderer.bounds.size.y, bkgd.transform.position.z);
        }
        return bkgd;
    }
}
