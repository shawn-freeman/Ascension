using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts.Abstract
{
    public class ForkMod : WeaponMod
    {
        public override bool OnHit(BulletScript projectile, params GameObject[] paramGameObjs)
        {
            //check to see if thisenemy has been hit already
            BasicEnemy enemy = paramGameObjs.FirstOrDefault(a => a.tag == "Enemy").GetComponent<BasicEnemy>();
            bool hasCollided = projectile.PreviouslyCollided.Any(a => a == enemy.gameObject);

            
            if (projectile.IsChild) return true;
            Debug.Log(string.Format("OnHit: {0} param: {1} ", projectile.name, paramGameObjs.FirstOrDefault()));
            if (!hasCollided)
            {
                Debug.Log(string.Format("hasNotCollided: ", enemy.name));
                projectile.PreviouslyCollided.Add(enemy.gameObject);

                BulletScript bullet = PoolManager.GetObject(LoadedAssets.PERIODIC_AOE_PREFAB).GetComponent<BulletScript>();
                bullet.Init(projectile.objOwner, projectile.CurrentAnimationValue, true);
                bullet.transform.position = projectile.transform.position;
                bullet.transform.rotation = projectile.transform.rotation;
                bullet.transform.Rotate(Vector3.forward, 30);
                bullet.nullTime = 0.1f;
                foreach (var mod in projectile.Mods)
                {
                    mod.OnCreate(bullet);
                }
                //base.OnCreate(bullet);

                bullet = PoolManager.GetObject(LoadedAssets.PERIODIC_AOE_PREFAB).GetComponent<BulletScript>();
                bullet.Init(projectile.objOwner, projectile.CurrentAnimationValue, true);
                bullet.transform.position = projectile.transform.position;
                bullet.transform.rotation = projectile.transform.rotation;
                bullet.transform.Rotate(Vector3.back, 30);
                bullet.nullTime = 0.1f;
                foreach (var mod in projectile.Mods)
                {
                    mod.OnCreate(bullet);
                }
                //base.OnCreate(bullet);

                return true;

            }

            return true;
        }
    }
}
