using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Resources.Scripts.Interfaces
{
    public interface IDamagable
    {
        bool OnDamage(float damage);
    }
}
