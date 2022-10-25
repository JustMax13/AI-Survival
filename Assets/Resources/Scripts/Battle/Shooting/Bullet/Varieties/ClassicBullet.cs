using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatMechanics
{
    using HP;
    using Weapon;

    public class ClassicBullet : AbstractBullet
    {
        [SerializeField]
        private float _damage;
        public override float Damage 
        { 
            get => _damage;
            set 
            {
                if (value < 0)
                    value = 0;
                _damage = value;
            } 
        }

        protected override LayerMask[] IgnoreLayers { get; set; }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            IgnoreLayers = new LayerMask[] {LayerMask.GetMask("Player"), LayerMask.GetMask("Invisible wall") };
            foreach (var item in IgnoreLayers)
                if (collision.gameObject.layer == item)
                    return;

            collision.gameObject.GetComponent<IHaveHP>()?.TakeDamage(collision.gameObject.GetComponent<IHaveHP>(), Damage);
            Destroy(gameObject);
        }
    }
}