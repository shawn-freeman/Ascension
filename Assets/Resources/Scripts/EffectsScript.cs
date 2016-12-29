using UnityEngine;
using System.Collections;
using Assets.Resources.Scripts.Enums;

public class EffectsScript : MonoBehaviour {

    private SpriteRenderer _renderer;
    private Animator _animator;

    private int _maxLoops = 1;
    private 
    
    void Awake()
    {
        Debug.Log("Awake");
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _animator = gameObject.GetComponent<Animator>();
    }
    void Start()
    {
        Debug.Log("Start");
    }

    public void Init(EFFECTS effect, int maxLoops)
    {
        Debug.Log("Init " + (_animator != null) + " " + effect );
        gameObject.SetActive(true);
        _animator.SetInteger(AnimationHashes.EFFECTS_ANIMATION_ID, (int)effect);
        _animator.speed = 0.5f;
        _maxLoops = maxLoops;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public void OnAnimationComplete()
    {

        var numLoops = (int)_animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        float playbackPercentage = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;

        if (_maxLoops == 1 && numLoops >= _maxLoops)
        {
            _animator.SetInteger(AnimationHashes.EFFECTS_ANIMATION_ID, -1);
            gameObject.SetActive(false);
        }
    }
}
