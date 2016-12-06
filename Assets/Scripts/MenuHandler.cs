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
}
