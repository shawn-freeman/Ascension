using UnityEngine;
using System.Collections;

public class EffectsScript : MonoBehaviour {

    private SpriteRenderer _renderer;
    private Animator _animator;

    private int _maxLoops = 1;
    // Use this for initialization
    void Start()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _animator = gameObject.GetComponent<Animator>();
    }

    public void Init(int maxLoops)
    {
        _animator.Play(AnimationHashes.EFFECT_BLUE_HIT);
        _maxLoops = maxLoops;
        gameObject.SetActive(true);
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
            gameObject.SetActive(false);
        }
    }
}
