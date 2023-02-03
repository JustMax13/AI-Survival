using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatMechanics
{
    namespace Weapon
    {
        public abstract class AbstractBullet : MonoBehaviour
        {
            public abstract float Damage { get; set; }
            public abstract List<LayerMask> IgnoreLayers { get; set; }
        }
    }
}