using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class FormationHandler : MonoBehaviour
{
    public List<GameObject> children;

    public void Init()
    {
        children = GetComponentsInChildren<Transform>()
            .Select(a => a.gameObject).ToList()
            .Where(b => b.tag.Equals("Enemy")).ToList();

        gameObject.SetActive(true);

        //Debug.Log(string.Format("Formation Activated {0} EnemyCount: {1}", name, children.Count));
    }
	
	// Update is called once per frame
	void Update ()
    {
        children = GetComponentsInChildren<Transform>()
            .Select(a => a.gameObject).ToList()
            .Where(b => b.tag.Equals("Enemy")).ToList();

        if (children == null || children.Where(a => !a.activeSelf).Count() >= children.Count)
        {
            //Debug.Log(string.Format("Formation Deactivated {0} ActiveCount: {1}", name, children.Count(a => a.activeSelf)));
            gameObject.SetActive(false);
        }
	}
}
