using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CombatMechanics
{
    using HP;
    public class HPOnObject : MonoBehaviour, IHaveHP
    {
        [SerializeField]
        [Range(0, 1000000)]
        private float _HP;
        public float HP
        {
            get { return _HP; }
            set { _HP = value; }
        }
        public void MakeDamage(float damage) => HP -= damage;

        virtual protected void FixedUpdate()
        {
            if (HP <= 0)
                Destroy(gameObject);
        }
    }
}