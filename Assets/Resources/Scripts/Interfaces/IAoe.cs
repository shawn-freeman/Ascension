using Assets.Resources.Scripts.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Resources.Scripts.Interfaces
{
    public interface IAoe
    {
        float CurrentScale { get; set; }
        float DefaultScale { get; set; }
        float SizeChangeRate { get; set; }
    }
}
