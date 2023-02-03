using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatMechanics
{
    using HP;
    using Weapon;

    public class ClassicBullet : AbstractBullet
    {
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

        public override List<LayerMask> IgnoreLayers { get; set; }

        private void Awake()
        {
            IgnoreLayers = new List<LayerMask>();
        }
        private void Start()
        {
            IgnoreLayers.Add(LayerMask.GetMask("Invisible wall"));
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            foreach (var item in IgnoreLayers)
                if (collision.gameObject.layer == item)
                    return;

            IHaveHP collisionHaveHP = collision.transform.GetComponent<IHaveHP>();
            collisionHaveHP?.TakeDamage(collisionHaveHP, Damage);

            Destroy(gameObject);
        }
    }
}