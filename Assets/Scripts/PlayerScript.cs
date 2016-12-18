using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	public const float SPEED = 5.0f;
	
	//public Gun currentWeapon;
	public float health;
	public float fireRate;
	public float nextFire;

	void Awake()
	{
	}
		
	// Use this for initialization
	void Start () 
	{
		fireRate = 0.1f;
		nextFire = 0.0f;
		//currentWeapon = new Gun();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Vector3 tempVector = new Vector3(Controller.inputMovement.x * SPEED * Time.deltaTime, 
		//								Controller.inputMovement.y * SPEED  * Time.deltaTime, 0);
		Vector3 tempVector = Controller.inputMovement * SPEED * Time.deltaTime;
        Debug.Log(Controller.inputMovement);
		//GetComponent<Rigidbody2D>().AddForce(tempVector);
		transform.position += tempVector;
		
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
		
		//if(transform.position.x > border2.x) transform.position = border2;
		if(Input.GetMouseButton(0) && Time.time >= nextFire) 
		{
			//FireBullet();
			nextFire = Time.time + fireRate;
		}
		//if(Input.GetMouseButton(0)) FireBullet();
	}
	
	//void FireBullet()
	//{	
	//	//vec1 = transform.position + vec1.normalized;
	//	Vector3 gunPosition1 = new Vector3(0.2f, 0, 0.1f);
	//	Vector3 gunPosition2 = new Vector3(-0.2f, 0, 0.1f);
		
	//	BulletScript bullet = PoolManager.GetObject (LoadedAssets.PREFAB_BULLET).GetComponent<BulletScript>();//(GameObject)Instantiate(LoadedAssets.objBullet, transform.position + gunPosition1, transform.rotation);
	//	bullet.transform.position = transform.position + gunPosition1;
	//	bullet.transform.rotation = transform.rotation;
	//	bullet.Init (this.gameObject);

	//	bullet = PoolManager.GetObject (LoadedAssets.PREFAB_BULLET).GetComponent<BulletScript>();//(GameObject)Instantiate(LoadedAssets.objBullet, transform.position + gunPosition1, transform.rotation);
	//	bullet.transform.position = transform.position + gunPosition2;
	//	bullet.transform.rotation = transform.rotation;
	//	bullet.Init (this.gameObject);
	//}
}
