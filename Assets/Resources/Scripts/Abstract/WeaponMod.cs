﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts.Abstract
{
    public abstract class WeaponMod : MonoBehaviour
    {
        public GameObject AttachedProjectile;

        public bool AttachedToProjectile {
            get
            {
                return AttachedProjectile == null;
            }
        }
        
        public virtual void OnCreate(BulletScript projectile) {
            projectile.Mods.Add(this);
        }

        public bool UsesOnHit;
        public virtual void OnHit(BulletScript projectile) { }

        public bool UsesOnAttack;
        public virtual void OnAttack() { }
    }
}
