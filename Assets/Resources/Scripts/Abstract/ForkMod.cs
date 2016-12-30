using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts.Abstract
{
    public class ForkMod : WeaponMod
    {
        public override void OnHit(BulletScript projectile)
        {
            BulletScript bullet = PoolManager.GetObject(LoadedAssets.PROJECTILE_PREFAB).GetComponent<BulletScript>();
            bullet.Init(projectile.objOwner, projectile.CurrentAnimationValue, true);
            bullet.transform.position = projectile.transform.position;
            bullet.transform.rotation = projectile.transform.rotation;
            bullet.transform.Rotate(Vector3.forward, 30);
            bullet.nullTime = 0.1f;
            base.OnCreate(bullet);

            bullet = PoolManager.GetObject(LoadedAssets.PROJECTILE_PREFAB).GetComponent<BulletScript>();
            bullet.Init(projectile.objOwner, projectile.CurrentAnimationValue, true);
            bullet.transform.position = projectile.transform.position;
            bullet.transform.rotation = projectile.transform.rotation;
            bullet.transform.Rotate(Vector3.back, 30);
            bullet.nullTime = 0.1f;
            base.OnCreate(bullet);
        }
    }
}
