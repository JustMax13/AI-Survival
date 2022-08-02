using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatMechanics
{
    namespace Weapon
    {
        interface IGunShot
        {
            Transform BulletSpawn { get; set; }
            GameObject BulletPrefab { get; set; }
            float BulletVelocity { get; set; }
            void Shot();
        }
    }
}