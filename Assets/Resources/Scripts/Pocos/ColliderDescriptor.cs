using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts.Pocos
{
    [Serializable]
    public class ColliderDescriptor
    {
        public int Id;
        public string Name;
        public Vector3 Size;
        public Vector3 Offset;
    }
}
