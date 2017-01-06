using UnityEngine;
using System.Collections;
using Assets.Resources.Scripts.Enums;
using Assets.Resources.Scripts.Interfaces;
using Assets.Resources;

public class BasicEnemy : ExtendedMonoBehavior, IDamagable
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

    public bool OnDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            EffectsScript effect = PoolManager.GetObject(LoadedAssets.EFFECTS_PREFAB).GetComponent<EffectsScript>();
            effect.transform.position = transform.position + Vector3.back;
            effect.transform.rotation = transform.rotation;
            effect.Init(EFFECTS.PurpleExplosion, 1);

            gameObject.SetActive(false);
            return true;
        }

        return false;
    }
}
