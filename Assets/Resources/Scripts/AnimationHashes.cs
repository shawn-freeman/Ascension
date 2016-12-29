using UnityEngine;
using System.Collections;

public class AnimationHashes {
    public static int MENU_InitiallyInActive = Animator.StringToHash("InitiallyInActive");
    public static int MENU_SwingDirection = Animator.StringToHash("SwingDirection");
    public static int MENU_ProfileSlideDirection = Animator.StringToHash("SlideDirection");

    public static int EFFECT_BLUE_HIT = Animator.StringToHash("Blue_Hit");
    public static int EFFECT_PURPLE_EXPLOSION = Animator.StringToHash("PurpleExplosion");

    public static int EFFECTS_ANIMATION_ID = Animator.StringToHash("EffectId");
}
