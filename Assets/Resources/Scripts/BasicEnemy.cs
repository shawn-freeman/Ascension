using UnityEngine;
using System.Collections;

public class BasicEnemy : MonoBehaviour
{
    public float MOVE_SPEED;

    public float Health = 10;
    public float MaxHealth = 10;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    void Start () {
	
	}

    public void Init()
    {
        Health = MaxHealth;
        gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update ()
    {
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

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + ((transform.up * MOVE_SPEED) * Time.deltaTime));
    }

    public void OnDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0) gameObject.SetActive(false);
    }
}
