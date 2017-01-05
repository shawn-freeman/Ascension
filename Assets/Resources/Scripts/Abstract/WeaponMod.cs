using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts.Abstract
{
    public abstract class WeaponMod
    {
        public virtual void OnCreate(BulletScript projectile) {
            projectile.Mods.Add(this);
        }
        public virtual bool OnHit(BulletScript projectile, params GameObject[] paramGameObj) { return false; }
        public virtual void OnAttack() { }
    }
}
