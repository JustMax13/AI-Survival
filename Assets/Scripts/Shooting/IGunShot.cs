using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
