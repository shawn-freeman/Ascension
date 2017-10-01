using Assets.Resources.Scripts.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Resources.Scripts.Interfaces
{
    public interface IModdable
    {
        List<WeaponMod> Mods { get; set; }
    }
}
