using Assets.Resources.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts.Abstract
{
    public class IncreaseAoeMod : WeaponMod
    {
        public IncreaseAoeMod(float val = 0.0f, float alt = 0.0f) :base(val, alt)
        {

        }
        public override void OnCreate(BulletScript projectile)
        {
            var aoe = projectile.GetInterfaceComponent<IAoe>();
            projectile.transform.localScale = Vector3.one * Value;

            projectile.IsImmortal = true;
            base.OnCreate(projectile);
        }
    }
}
