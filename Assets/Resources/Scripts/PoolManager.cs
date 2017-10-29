using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour 
{
	private static Dictionary<Object, List<GameObject>> pools;

	// Use this for initialization
	void Start () 
	{
		pools = new Dictionary<Object, List<GameObject>> ();
	}

	public static GameObject GetObject(Object objToCreate)
	{
		if (!pools.ContainsKey (objToCreate)) 
		{
			//create a new list of objects to create where the key is the object type to create
			pools.Add(objToCreate, new List<GameObject>());
		}

		//get a reference to the pool of objects we are trying to create
		List<GameObject> pool = pools [objToCreate];

		foreach (GameObject gameObj in pool) 
		{
			//check if the current object
			if(!gameObj.activeSelf) return gameObj;
		}

		//if point has been reached, create a new gameobject
		GameObject newGameObj = (GameObject)Instantiate (objToCreate);
		newGameObj.name = string.Format ("{0} ID: {1} ", ((GameObject)objToCreate).name, pool.Count);
		pool.Add(newGameObj);
		return newGameObj;
	}

    public static GameObject GetObject(Object objToCreate, GameObject container)
    {
        if (!pools.ContainsKey(objToCreate))
        {
            //create a new list of objects to create where the key is the object type to create
            pools.Add(objToCreate, new List<GameObject>());
        }

        //get a reference to the pool of objects we are trying to create
        List<GameObject> pool = pools[objToCreate];

        foreach (GameObject gameObj in pool)
        {
            //check if the current object
            if (!gameObj.activeSelf) return gameObj;
        }

        //if point has been reached, create a new gameobject
        GameObject newGameObj = (GameObject)Instantiate(objToCreate, container.transform, false);

        newGameObj.name = string.Format("{0} ID: {1} ", ((GameObject)objToCreate).name, pool.Count);
        pool.Add(newGameObj);
        return newGameObj;
    }
}
