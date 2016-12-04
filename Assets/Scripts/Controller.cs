using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	
	public static Vector3 inputMovement;
	public static Vector3 mousePosition;
	public static float compassDegree;
	public bool isMobile;
	// Use this for initialization
	void Start () 
	{
		if(Input.acceleration != null) isMobile = true;
		
		if(isMobile) Input.location.Start();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//get position of the mouse on the screen
		mousePosition = Input.mousePosition;
		
		if(isMobile) compassDegree = Mathf.Round(Input.compass.trueHeading);
		
		//check to use mobile
		if(Input.acceleration.x != 0 || Input.acceleration.y != 0)
		{
			inputMovement = Input.acceleration;
			//transfer y values to the z input
			if(inputMovement.x <= 0.1 && inputMovement.x >= -0.1) { inputMovement.x = 0; }
			if(inputMovement.y <= 0.1 && inputMovement.y >= -0.1) { inputMovement.y = 0; }
			inputMovement.z = 0;
		}else{
			//keyboard movement
			inputMovement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		}
	}
}
