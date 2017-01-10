using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Controller : MonoBehaviour {
	
	public static Vector3 inputMovement;
	public static Vector3 mousePosition;
	public static float compassDegree;
	public bool isMobile;

    private Dictionary<string, List<Action<Vector3>>> _inputCallbacks;

    private static Controller _instance;
    public static Controller _Instance
    {
        get
        {
            //check that the singleton has not been set yet
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Controller>();

                //no object with this component exists, throw an error
                if (_instance == null)
                {
                    throw new Exception("Could not find a gameobject with the Controller component.");
                }
            }

            return _instance;
        }
    }

    void Awake()
    {
        _inputCallbacks = new Dictionary<string, List<Action<Vector3>>>();

        if (Input.acceleration != null) isMobile = true;

        if (isMobile) Input.location.Start();
    }

    // Use this for initialization
    void Start () 
	{
		
	}

    public void RegisterInput<T>(Action<Vector3> inputCallback, KeyCode keyCode)
    {
        var strKey = keyCode.ToString();
        List<Action<Vector3>> temp;

        if (!_inputCallbacks.ContainsKey(strKey))
        {
            _inputCallbacks[strKey] = new List<Action<Vector3>>();
        }

        temp = _inputCallbacks[strKey];
        temp.Add(inputCallback);
        
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

            var keys = _inputCallbacks.Keys;
            foreach (var key in keys)
            {
                if (Input.GetKeyDown(key.ToLower()))
                {
                    var callbacks = _inputCallbacks[key];

                    foreach (var callback in callbacks)
                    {
                        callback(inputMovement);
                    }
                }
            }
		}
	}
}
