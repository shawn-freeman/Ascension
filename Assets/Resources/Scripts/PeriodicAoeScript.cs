using Assets.Resources.Scripts.Enums;
using Assets.Resources.Scripts.Interfaces;
using Assets.Resources.Scripts.Structs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts
{
    public class PeriodicAoeScript : BulletScript, IAoe
    {
        public float CurrentScale { get; set; }
        public float DefaultScale { get; set; }
        public float SizeChangeRate { get; set; }
        public bool canDamage;

        public override void Init(GameObject owner, int animation, bool isChild = false)
        {
            base.Init(owner, animation, isChild);

            CurrentScale = 1;
            DefaultScale = 1;
            SizeChangeRate = 0;

            _collider.enabled = false;
        }

        public override void Update()
        {
            var sprite = GetComponent<SpriteRenderer>();
            base.Update();

            if (gameObject.activeInHierarchy)
            {
                if (_collider.enabled)
                {
                    StartCoroutine(Deactivate());
                }
                else { StartCoroutine(Activate()); }
            }
        }

        public void OnTriggerStay2D(Collider2D collision)
        {
            bool performOnHit = false;

            if (timeSpentAlive < nullTime) return;

            var damagable = collision.gameObject.GetComponent<IDamagable>();

            if (damagable == null || damagable == objOwner.GetComponent<IDamagable>()) return;

            EffectsScript effect = PoolManager.GetObject(LoadedAssets.EFFECTS_PREFAB).GetComponent<EffectsScript>();
            effect.transform.position = collision.gameObject.transform.position;
            effect.transform.rotation = transform.rotation;
            effect.Init(EFFECTS.BlueHit, 1);

            damagable.OnDamage(Damage);

            if (!IsImmortal) gameObject.SetActive(false);
        }

        public IEnumerator Deactivate()
        {
            _collider.enabled = false;

            yield return new WaitForSeconds(0.5f);
        }

        public IEnumerator Activate()
        {
            _collider.enabled = true;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
