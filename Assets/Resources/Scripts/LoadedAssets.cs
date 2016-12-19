using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Linq;
using System.IO;

public class LoadedAssets : MonoBehaviour {

	#region Actor Prefabs
	public static GameObject PREFAB_BULLET;
    public static GameObject ALIENTIER1_SHIP_1;
    public static GameObject FORMATION_DIAMOND;


    #endregion

    #region Effect Prefabs

    #endregion

    #region Sound Effects

    public static AudioClip 			SFX_LASER;
	public static AudioClip 			SFX_ENERGY_EXPLOSION;
	public static AudioClip 			SFX_POWER_UP;

	#endregion

	void Awake()
	{
		PREFAB_BULLET = (GameObject)Resources.Load("Prefabs/GatlingLaser", typeof(GameObject));
        ALIENTIER1_SHIP_1 = (GameObject)Resources.Load("Prefabs/AlienTier1_Ship1", typeof(GameObject));
        FORMATION_DIAMOND = (GameObject)Resources.Load("Prefabs/Formation_Diamond", typeof(GameObject));

        Debug.Log("Loaded Assets!");
    }
}