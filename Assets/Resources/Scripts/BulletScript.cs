using UnityEngine;
using System.Collections;
using Assets.Resources.Scripts.Enums;
using Assets.Resources.Scripts.Abstract;
using System.Collections.Generic;
using System;
using Assets.Resources.Scripts.Pocos;
using System.Linq;
using Assets.Resources;
using Assets.Resources.Scripts.Interfaces;

public class BulletScript : ExtendedMonoBehavior, IModdable
{
	
	public float moveSpeed = 0.5f;		//how fast the bullet moves
	public float timeSpentAlive;        //how long the bullet has existed
    public GameObject objOwner;
    public GameObject HitEffect;

    public bool IsChild = false;
    public bool IsImmortal;
    public float MaxLifeTime = 3;
    public float Damage = 5;

    public List<WeaponMod> Mods { get; set; }

    public float nullTime;
    
    public List<ColliderDescriptor> ColliderPresets;
    public List<GameObject> PreviouslyCollided;


    public int CurrentAnimationValue
    {
        get {
            return _animator.GetInteger(AnimationHashes.PROJECTILE_ANIMATION_ID);
        }
    }

    protected Rigidbody2D _rigidbody;
    protected Animator _animator;
    protected BoxCollider2D _collider;

    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
    }
    void Start () 
	{
        
	}
	
	public virtual void Init(GameObject owner, int animation, bool isChild = false)
	{
		objOwner = owner;
        transform.rotation = objOwner.transform.rotation;
        //AudioSource.PlayClipAtPoint(LoadedAssets.SFX_LASER, Camera.main.transform.position);
        timeSpentAlive = 0;
        IsChild = isChild;
        nullTime = isChild ? 0.025f : 0.0f;
        transform.localScale = Vector3.one;

        
        IsImmortal = false;
        gameObject.SetActive (true);
        Mods = new List<WeaponMod>();
        _animator.SetInteger(AnimationHashes.PROJECTILE_ANIMATION_ID, animation);
        _collider.size = ColliderPresets.FirstOrDefault(a => a.Id == animation).Size;
        _collider.offset = ColliderPresets.FirstOrDefault(a => a.Id == animation).Offset;
    }
	
	public void dispose()
	{
        //if(hitEffect != null) Destroy(hitEffect.gameObject, hitEffect.duration);
        objOwner = null;
        Mods = null;
        PreviouslyCollided = new List<GameObject>();
        _animator.SetInteger(AnimationHashes.PROJECTILE_ANIMATION_ID, -1);
        gameObject.SetActive (false);
    }
	
	// Update is called once per frame
	public virtual void Update () 
	{
        
        timeSpentAlive += Time.deltaTime;

        //if bullet has been traveling for more than one second
        if (timeSpentAlive > MaxLifeTime) dispose();
	}

    public void FixedUpdate()
    {
        Vector3 moveVector = transform.position + ((transform.up * moveSpeed) * Time.deltaTime);
        _rigidbody.MovePosition(moveVector);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        bool performOnHit = false;

        if (timeSpentAlive < nullTime) return;

        var damagable = collision.gameObject.GetComponent<IDamagable>();

        if (damagable == null || damagable == objOwner.GetComponent<IDamagable>()) return;

        performOnHit = OnHitMods(collision);

        if (performOnHit)
        {
            EffectsScript effect = PoolManager.GetObject(LoadedAssets.EFFECTS_PREFAB).GetComponent<EffectsScript>();
            effect.transform.position = transform.position;
            effect.transform.rotation = transform.rotation;
            effect.Init(EFFECTS.BlueHit, 1);

            var isDestroyed =  damagable.OnDamage(Damage);

            if (isDestroyed)
            {
                PreviouslyCollided.Remove(collision.gameObject);
            }
        }

        if (!IsImmortal) gameObject.SetActive(false);
    }

    public bool OnHitMods(Collider2D collision)
    {
        var boolVals = new List<bool>();
        foreach (var mod in Mods)
        {
            boolVals.Add(mod.OnHit(this, collision.gameObject));
        }

        return boolVals.Any(a => a == true);
    }
}
