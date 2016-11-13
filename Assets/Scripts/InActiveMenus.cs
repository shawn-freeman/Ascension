using UnityEngine;
using System.Collections;

public class InActiveMenus : MonoBehaviour {

    private GameObject Panel;
    private Animator PanelAnimator;


    void Awake()
    {
        Panel = this.gameObject;
        PanelAnimator = Panel.GetComponent<Animator>();
        PanelAnimator.SetBool(AnimationHashes.MENU_InitiallyInActive, true);
    }

    // Use this for initialization
    void Start ()
    {
        
    }

    void AfterInitialInActive()
    {
        PanelAnimator.SetBool(AnimationHashes.MENU_InitiallyInActive, false);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
