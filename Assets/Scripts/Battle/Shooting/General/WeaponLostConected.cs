using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatMechanics
{
    namespace Weapon
    {
        public class WeaponLostConected : MonoBehaviour
        {
            private void FixedUpdate()
            {
                if (GetComponent<FixedJoint2D>().connectedBody == null)
                {
                    Destroy(GetComponent<ShotGun>());
                    Destroy(GetComponent<FixedJoint2D>());
                    Destroy(this);
                }
            }
        }
    }
}