using Assets.Resources.Scripts.Abstract;
using Assets.Resources.Scripts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts.Pocos
{
    public class Weapon
    {
        public GameObject Owner;
        public List<Vector3> ProjectilePositions;
        public List<WeaponMod> WeaponMods;

        public GameObject ProjectilePrefab;

        private float AttackRate = 0.5f;
        public float LastAttack = 0.0f;
        public float Damage;

        public void Init(GameObject owner, List<Vector3> positions)
        {
            this.Owner = owner;
            ProjectilePositions = positions;

            WeaponMods = new List<WeaponMod>() { new ForkMod() };

            ProjectilePrefab = LoadedAssets.BLUE_PLASMA;
        }

        public void Attack()
        {
            if (Time.time < LastAttack + AttackRate) return;
            
            foreach (var position in ProjectilePositions)
            {
                BulletScript bullet = PoolManager.GetObject(LoadedAssets.PROJECTILE_PREFAB).GetComponent<BulletScript>();
                bullet.transform.position = Owner.transform.position + position;
                bullet.transform.rotation = Owner.transform.rotation;
                bullet.Init(Owner.gameObject, (int)ProjectileAnimationValues.BluePlasma);

                foreach (var mod in WeaponMods)
                {
                    mod.OnCreate(bullet);
                }
            }

            LastAttack = Time.time;
        }
    }
}
