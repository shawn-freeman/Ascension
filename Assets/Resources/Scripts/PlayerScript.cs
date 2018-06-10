using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Assets.Scripts.Constants;
using Assets.Resources.Scripts.Pocos;
using System.Collections.Generic;
using Assets.Resources.Scripts.Interfaces;
using Assets.Resources;
using Assets.Resources.Scripts.Enums;

public class PlayerScript : ExtendedMonoBehavior, IDamagable
{	
	public const float SPEED = 5.0f;
	
	//public Gun currentWeapon;
	public float health;

    public Weapon CurrentWeapon;

	void Awake()
	{

    }
		
	// Use this for initialization
	void Start () 
	{
        CurrentWeapon = new Weapon();
        CurrentWeapon.Init(this.gameObject, 
                            LoadedAssets.PROJECTILE_PREFAB,
                            new List<Vector3>() { Vector3.zero }
                            //new List<Vector3>() { new Vector3(0.2f, 0, 0.1f), new Vector3(-0.2f, 0, 0.1f) } //two blasters
                            );
        CurrentWeapon.AnimationValue = 1;
        CurrentWeapon.AttackRate = 0.3f;

        Controller._Instance.RegisterInput<Vector3>(PrevWeapon, KeyCode.Q);
        Controller._Instance.RegisterInput<Vector3>(NextWeapon, KeyCode.E);
    }
	
	// Update is called once per frame
	void Update () 
	{
        //Vector3 moveVector = Controller.inputMovement * SPEED * Time.deltaTime;

        //transform.position += moveVector;

        if (Input.GetMouseButton(0))
        {
            var heading = Camera.main.ScreenToWorldPoint(Controller.mousePosition) - gameObject.transform.position;
            var dist = heading.magnitude > SPEED ? SPEED : heading.magnitude;
            var direction = heading / dist;

            //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Controller.mousePosition);
            direction.z = 0;
            var accelerate = (direction / 10).normalized;

            transform.position += direction;
            CurrentWeapon.Attack();
        }

        //get the distance on the Z axis from the main cmaera to the player
        float distCamToPlayer = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
		//convert coordinates to world space
		Vector3 leftBorder = Camera.main.ScreenToWorldPoint(new Vector3(gameObject.transform.lossyScale.x, 0, distCamToPlayer));
		Vector3 rightBorder = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - gameObject.transform.lossyScale.x, 0, distCamToPlayer));
		Vector3 bottomBorder = Camera.main.ScreenToWorldPoint(new Vector3(0, gameObject.transform.lossyScale.z, distCamToPlayer));
		Vector3 topBorder = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height - gameObject.transform.lossyScale.z, distCamToPlayer)); 
		
		//check if the player is beyond the left or right border
		if(transform.position.x < leftBorder.x || transform.position.x > rightBorder.x) 
		{
			Vector3 v = transform.position;				//temp variable
			v.x = Mathf.Clamp(v.x, leftBorder.x, rightBorder.x);//clamp the x value to within the border value
			transform.position = v;
		}
		
		//check if the player is beyond the top or bottom border
		if(transform.position.y < bottomBorder.y || transform.position.y > topBorder.y)
		{
			Vector3 v = transform.position;
			v.y = Mathf.Clamp(v.y, bottomBorder.y, topBorder.y);
			transform.position = v;
		}

        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Enemy"))
        {
            OnDamage(1);
        }
    }

    public bool OnDamage(float damage)
    {
        health -= damage;

        EffectsScript effect = PoolManager.GetObject(LoadedAssets.EFFECTS_PREFAB).GetComponent<EffectsScript>();
        effect.transform.position = transform.position;
        effect.transform.rotation = transform.rotation;
        effect.Init(EFFECTS.BlueHit, 1);

        if (health <= 0) {
            SceneManager.LoadScene(Scenes.MAIN_MENU);
            return true;
        }

        return false;
    }

    public void PrevWeapon(Vector3 input)
    {
        CurrentWeapon = new Weapon();
        CurrentWeapon.Init(this.gameObject,
                            LoadedAssets.PERIODIC_AOE_PREFAB,
                            new List<Vector3>() { Vector3.zero }
                            );
        CurrentWeapon.AnimationValue = 2;
    }

    public void NextWeapon(Vector3 input)
    {
        CurrentWeapon = new Weapon();
        CurrentWeapon.Init(this.gameObject,
                            LoadedAssets.PROJECTILE_PREFAB,
                            new List<Vector3>() { Vector3.zero }
                            );
        CurrentWeapon.AnimationValue = 1;
        CurrentWeapon.AttackRate = 0.3f;
    }
}
