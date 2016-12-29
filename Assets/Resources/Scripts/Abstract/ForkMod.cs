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
            var rotation = projectile.gameObject.transform.rotation;

            BulletScript bullet = PoolManager.GetObject(LoadedAssets.PREFAB_BULLET).GetComponent<BulletScript>();
            bullet.transform.position = projectile.transform.transform.position + (Vector3.left / 2);
            //bullet.transform.rotation = projectile.transform.rotation. + Vector3.
            //bullet.transform.Rotate(0, -45, -45);
            bullet.Init(projectile.objOwner);
            bullet.nullTime = 0.1f;
            base.OnCreate(bullet);

            bullet = PoolManager.GetObject(LoadedAssets.PREFAB_BULLET).GetComponent<BulletScript>();
            bullet.transform.position = projectile.transform.transform.position + (Vector3.right / 2);
            //bullet.transform.rotation = projectile.transform.rotation;
            //bullet.transform.Rotate(0, 45, 45);
            bullet.Init(projectile.objOwner);
            bullet.nullTime = 0.1f;
            base.OnCreate(bullet);
        }
    }
}
