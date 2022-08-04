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
            protected abstract LayerMask[] IgnoreLayers { get; set; }
        }
    }
}