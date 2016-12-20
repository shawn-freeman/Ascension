using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public PoolManager poolManager;
    public float timeToNext;
    public float waveIntervals;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    timeToNext += Time.deltaTime;

        if (timeToNext >= waveIntervals)
        {
            GameObject formation = PoolManager.GetObject(LoadedAssets.FORMATION_DIAMOND);
            formation.transform.position = new Vector3(0, 6, 2);  //assign off the top of the screen
            List<Transform> positions = formation.GetComponentsInChildren<Transform>().Where(b => b.name.Contains("Position") && b.gameObject.activeSelf).ToList();
            float lowest = positions.Max(a => a.transform.position.y);

            positions.Select(a =>
            {
                BasicEnemy enemy = PoolManager.GetObject(LoadedAssets.ALIENTIER1_SHIP_1).GetComponent<BasicEnemy>();
                //Debug.Log(string.Format("{0} {1} {2}", enemy.name, a.position, a.rotation));
                enemy.transform.SetParent(formation.transform);
                enemy.transform.position = a.position;
                enemy.transform.rotation = a.rotation;
                enemy.Init();
                return enemy;
            }).ToList();

            formation.GetComponent<FormationHandler>().Init();

            timeToNext = 0;
        }
    }
}
