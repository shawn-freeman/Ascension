using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	
	public float moveSpeed = 0.5f;		//how fast the bullet moves
	public float timeSpentAlive;		//how long the bullet has existed
	public GameObject objOwner;
	public ParticleSystem hitEffect;

    public float MaxLifeTime = 3;

	// Use this for initialization
	void Start () 
	{

	}
	
	public void Init(GameObject owner)
	{
		objOwner = owner;
		//AudioSource.PlayClipAtPoint(LoadedAssets.SFX_LASER, Camera.main.transform.position);
		timeSpentAlive = 0;
		gameObject.SetActive (true);
	}

	
	public void dispose()
	{
		if(hitEffect != null) Destroy(hitEffect.gameObject, hitEffect.duration);

		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeSpentAlive += Time.deltaTime;
		
		//if bullet has been traveling for more than one second
		if(timeSpentAlive > MaxLifeTime) dispose();
        
        transform.rotation = objOwner.transform.rotation;
		transform.Translate((transform.up * moveSpeed) * Time.deltaTime);
		
		RaycastHit hit;
		float distance = 0.5f;
		
		//check for collsion against the ray
		if(Physics.Raycast(transform.position, transform.forward, out hit, distance))
		{	
			//the game object 
			GameObject collided = hit.collider.gameObject;
			
			//make sure projectile has not collided with another projectile
			if(collided.tag != "Projectile")
			{
				//if the bullet hit an enemy
				if(collided.tag == "Enemy") 
				{
					//Enemy fighter = hit.collider.gameObject.GetComponent<Enemy>();
					//fighter.health -= 10;
					//EffectsAnimator effect = PoolManager.GetObject(LoadedAssets.PREFAB_EFFECTS.gameObject).GetComponent<EffectsAnimator>();
					//effect.transform.position = transform.position;
					//effect.transform.rotation = transform.rotation;
					//effect.SetAnimation(EffectsAnimator.EffectAnimations.BLUE_HIT);

					//hitEffect = (ParticleSystem)Instantiate(LoadedAssets.PARTICLE_BULLET_HIT, transform.position, Quaternion.identity);
					//dispose();
				}
			}
		}
		
	}
}
