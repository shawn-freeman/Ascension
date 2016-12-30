using UnityEngine;
using System.Collections;
using Assets.Resources.Scripts.Enums;
using Assets.Resources.Scripts.Abstract;
using System.Collections.Generic;
using System;
using Assets.Resources.Scripts.Pocos;
using System.Linq;

public class BulletScript : MonoBehaviour {
	
	public float moveSpeed = 0.5f;		//how fast the bullet moves
	public float timeSpentAlive;		//how long the bullet has existed
	public GameObject objOwner;

	public GameObject HitEffect;

    public float MaxLifeTime = 3;
    public float Damage = 5;

    public List<WeaponMod> Mods;

    public float nullTime;
    
    public List<ColliderDescriptor> ColliderPresets;

    public int CurrentAnimationValue
    {
        get {
            return _animator.GetInteger(AnimationHashes.PROJECTILE_ANIMATION_ID);
        }
    }

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private BoxCollider2D _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
    }
    void Start () 
	{
        
	}
	
	public void Init(GameObject owner, int animation, bool isChild = false)
	{
		objOwner = owner;
        transform.rotation = objOwner.transform.rotation;
        //AudioSource.PlayClipAtPoint(LoadedAssets.SFX_LASER, Camera.main.transform.position);
        timeSpentAlive = 0;
        nullTime = isChild ? 0.025f : 0.0f;

        

        gameObject.SetActive (true);

        _animator.SetInteger(AnimationHashes.PROJECTILE_ANIMATION_ID, animation);
        _collider.size = ColliderPresets.FirstOrDefault(a => a.Id == animation).Size;
        _collider.offset = ColliderPresets.FirstOrDefault(a => a.Id == animation).Offset;
    }
	
	public void dispose()
	{
        //if(hitEffect != null) Destroy(hitEffect.gameObject, hitEffect.duration);
        objOwner = null;
        Mods = new List<WeaponMod>();
        _animator.SetInteger(AnimationHashes.PROJECTILE_ANIMATION_ID, -1);
        gameObject.SetActive (false);
    }
	
	// Update is called once per frame
	void Update () 
	{
        
        timeSpentAlive += Time.deltaTime;

        //if bullet has been traveling for more than one second
        if (timeSpentAlive > MaxLifeTime) dispose();

		RaycastHit hit;
		float distance = 0.5f;
		
		//check for collsion against the ray
		//if(Physics.Raycast(transform.position, transform.up, out hit, distance))
		//{	
		//	//the game object 
		//	GameObject collided = hit.collider.gameObject;
			
		//	//make sure projectile has not collided with another projectile
		//	if(collided.tag != "Projectile")
		//	{
		//		//if the bullet hit an enemy
		//		if(collided.tag == "Enemy") 
		//		{
		//			BasicEnemy enemy = hit.collider.gameObject.GetComponent<BasicEnemy>();
  //                  enemy.OnDamage(10);
  //                  //EffectsAnimator effect = PoolManager.GetObject(LoadedAssets.PREFAB_EFFECTS.gameObject).GetComponent<EffectsAnimator>();
  //                  //effect.transform.position = transform.position;
  //                  //effect.transform.rotation = transform.rotation;
  //                  //effect.SetAnimation(EffectsAnimator.EffectAnimations.BLUE_HIT);

  //                  //hitEffect = (ParticleSystem)Instantiate(LoadedAssets.PARTICLE_BULLET_HIT, transform.position, Quaternion.identity);
  //                  dispose();
		//		}
		//	}
		//}

        
		
	}

    private void FixedUpdate()
    {
        Vector3 moveVector = transform.position + ((transform.up * moveSpeed) * Time.deltaTime);
        _rigidbody.MovePosition(moveVector);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.GetComponent<PlayerScript>())
        {

        }else
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (timeSpentAlive < nullTime) return;
        if (collision.gameObject.GetComponent<PlayerScript>())
        {
            Debug.Log("Colliding with Player");
        }
        else if(collision.gameObject.GetComponent<BasicEnemy>())
        {
            foreach (var mod in Mods)
            {
                mod.OnHit(this);
            }
            EffectsScript effect = PoolManager.GetObject(LoadedAssets.EFFECTS_PREFAB).GetComponent<EffectsScript>();
            effect.transform.position = transform.position;
            effect.transform.rotation = transform.rotation;
            effect.Init(EFFECTS.BlueHit, 1);

            BasicEnemy enemy = collision.gameObject.GetComponent<BasicEnemy>();
            enemy.OnDamage(Damage);
            gameObject.SetActive(false);
        }
    }
}
