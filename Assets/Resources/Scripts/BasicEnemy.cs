using UnityEngine;
using System.Collections;

public class BasicEnemy : MonoBehaviour
{
    public float MOVE_SPEED;

	// Use this for initialization
	void Start () {
	
	}

    public void Init()
    {
        gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += (transform.up * MOVE_SPEED) * Time.deltaTime;

        float camDistToThis = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        Vector3 screenToPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, camDistToThis));

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        //check if top of sprite has reached off the screen
        if (transform.position.y + renderer.bounds.extents.y <= screenToPoint.y)
        {
            transform.parent = null;
            gameObject.SetActive(false);
        }
    }
}
