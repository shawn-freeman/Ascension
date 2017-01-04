using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts.Abstract
{
    public class PierceMod :WeaponMod
    {
        public override void OnCreate(BulletScript projectile)
        {
            projectile.IsImmortal = true;
            base.OnCreate(projectile);
        }

        public override bool OnHit(BulletScript projectile, params GameObject[] paramGameObjs)
        {
            BasicEnemy enemy = paramGameObjs.FirstOrDefault(a => a.tag == "Enemy").GetComponent<BasicEnemy>();
            bool hasNotCollided = projectile.PreviouslyCollided.Any(a => a != enemy.gameObject);

            if (hasNotCollided)
            {
                projectile.PreviouslyCollided.Add(enemy.gameObject);
                return true;
            }

            return false;
        }
    }
}
