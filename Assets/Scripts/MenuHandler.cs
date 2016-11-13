using UnityEngine;
using System.Collections;

public class MenuHandler : MonoBehaviour {

    public bool initiallyInActive = false;

    private GameObject Panel;
    private Animator PanelAnimator;


    void Awake()
    {
        Panel = this.gameObject;
        PanelAnimator = Panel.GetComponent<Animator>();

        if (initiallyInActive)
        {
            PanelAnimator.SetBool(AnimationHashes.MENU_InitiallyInActive, true);
        }
    }

    // Use this for initialization
    void Start() { }
    // Update is called once per frame
    void Update() { }

    void OnInActiveComplete()
    {
        PanelAnimator.SetBool(AnimationHashes.MENU_InitiallyInActive, false);
    }

    void OnViewToFrontComplete()
    {
        PanelAnimator.SetBool(AnimationHashes.MENU_SwingViewToFront, false);
    }

    void OnFrontToViewComplete()
    {
        PanelAnimator.SetBool(AnimationHashes.MENU_SwingFrontToView, false);
    }
}
