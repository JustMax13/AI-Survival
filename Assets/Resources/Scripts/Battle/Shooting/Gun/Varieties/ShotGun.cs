using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatMechanics
{
    using Weapon;
    public class ShotGun : GunShot
    {
       
        protected override void Shot()
        {
            GameObject newBullet = Instantiate(BulletPrefab, BulletSpawn.transform.position, BulletSpawn.transform.rotation);

            try
            {
                newBullet.GetComponent<Rigidbody2D>().velocity = BulletVelocity * BulletSpawn.right;

                var classicBullet = newBullet.GetComponent<AbstractBullet>();
                classicBullet.Damage = BulletDamage;
                classicBullet.IgnoreLayers.Add(gameObject.layer);
            }
            catch
            {
                Destroy(newBullet);
                throw new System.Exception("The generated bullet does not have a Rigidbody 2d or ClassicBullet component");
            }
            finally { CurrentProjectilesInAClip -= 1; }
        }
    }
}