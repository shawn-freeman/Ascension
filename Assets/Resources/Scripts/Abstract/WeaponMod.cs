using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts.Abstract
{
    public abstract class WeaponMod
    {
        public float Value;
        public float AlternateValue;

        //public WeaponMod() {  }
        //public WeaponMod(float val = 0.0f)
        //{
        //    Value = val;
        //}
        public WeaponMod(float val = 0.0f, float alt = 0.0f)
        {
            Value = val;
            AlternateValue = alt;
        }

        public virtual void OnCreate(BulletScript projectile) {
            projectile.Mods.Add(this);
        }
        public virtual bool OnHit(BulletScript projectile, params GameObject[] paramGameObjs) { return true; }
        public virtual void OnAttack() { }
    }
}
